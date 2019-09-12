using System;
using System.Linq;
using CodeWriter;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeGen
{
    internal class DbContextCodeGenerator
    {
        public Options Options { get; set; }

        public void GenerateCode(InterfaceDeclarationSyntax idecl, CodeWriter.CodeWriter w)
        {
            var iname = idecl.Identifier.ToString();
            Console.WriteLine("GenerateCode: " + iname);

            w._($"#region {iname}DbContext");
            w._();

            var namespaceScope = idecl.GetNamespaceScope();
            var namespaceHandle = (string.IsNullOrEmpty(namespaceScope) == false)
                                      ? w.B($"namespace {idecl.GetNamespaceScope()}")
                                      : null;

            GenerateDbContextCode(idecl, w);

            namespaceHandle?.Dispose();

            w._();
            w._($"#endregion");
        }

        private void GenerateDbContextCode(InterfaceDeclarationSyntax idecl, CodeWriter.CodeWriter w)
        {
            var typeName = idecl.GetTypeName();
            var className = typeName.Substring(1);

            var properties = idecl.GetProperties();

            w._(idecl.AttributeLists.ToString());
            using (w.b($"public partial class {className}"))
            {
                // Property Accessors
                foreach (var p in properties)
                {
                    var propertyType = p.Type.ToString();
                    var propertyName = p.Identifier.ToString();
                    if (p.AttributeLists.Count > 0)
                        w._(p.AttributeLists.ToString());

                    w._($"public {propertyType} {propertyName} {{ get; set; }}");
                }
            }
        }
    }
}
