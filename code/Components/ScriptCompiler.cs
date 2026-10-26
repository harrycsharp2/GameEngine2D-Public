using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Windows;
namespace GameEngine2D.Components
{
    public static class ScriptCompiler
    {
        //called with filepath
        public static Type? CompileScriptFromFile(string filePath)
        {
            //checking if file exists 
            if (!File.Exists(filePath))
            {
                MessageBox.Show($"File not found: {filePath}");
                return null;
            }
            //if file exists continue

            //reading file
            string sourceCode = File.ReadAllText(filePath);
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
            
            // Collect all references currently loaded by the game engine (WPF, System, Vectors, etc.)
            var references = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
                .Select(a => (MetadataReference)MetadataReference.CreateFromFile(a.Location))
                .ToList();

            string assemblyName = Path.GetFileNameWithoutExtension(filePath) + "_" + Guid.NewGuid().ToString("N");
            var compilation = CSharpCompilation.Create(
                assemblyName,
                new[] { syntaxTree },
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
            );
            // Compile into memory 
            using var ms = new MemoryStream();
            var emitResult = compilation.Emit(ms);
            if (!emitResult.Success)
            {
                // Show errors if the user wrote invalid C#
                var errors = string.Join("\n", emitResult.Diagnostics
                    .Where(d => d.Severity == DiagnosticSeverity.Error)
                    .Select(d => d.GetMessage()));
                MessageBox.Show($"Compilation failed for {Path.GetFileName(filePath)}:\n\n{errors}");
                return null;
            }
            ms.Seek(0, SeekOrigin.Begin);
            Assembly compiledAssembly = AssemblyLoadContext.Default.LoadFromStream(ms);
            // Find the class inside the script that inherits from Script
            return compiledAssembly.GetTypes()
                .FirstOrDefault(t => typeof(Script).IsAssignableFrom(t) && !t.IsAbstract);
        }
    }
}