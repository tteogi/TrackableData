using System;
using System.Collections.Generic;
using System.Linq;
using CodeWriter;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeGen
{
    internal class EntityFrameworkCodeGenerator
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

            using (w.b($"public partial class {className}"))
            {
                // Property Accessors
                foreach (var p in properties)
                {
                    var propertyType = p.Type.ToString();
                    var propertyName = p.Identifier.ToString();
                    var ignore = p.AttributeLists.GetAttribute("IgnoreEntityFrameworkModel");
                    if (ignore != null)
                        continue;
                    var entityFrameworkAttribute = p.AttributeLists.GetAttribute("EntityFrameworkModelAttribute");
//                        List<AttributeSyntax> attributeSyntaxes = new List<AttributeSyntax>();
//                        foreach (var attributeListSyntax in p.AttributeLists)
//                        {
//                            foreach (var attribute in attributeListSyntax.Attributes)
//                            {
//                                attributeSyntaxes.Add(attribute);
//                                Console.WriteLine(attribute);
//                            }
//                        }
//                        foreach (var attributeSyntax in attributeSyntaxes)
//                        {
                    if (entityFrameworkAttribute != null)
                    {
                        var args = entityFrameworkAttribute.ArgumentList.Arguments.ToString();
                        if (args.StartsWith("@"))
                            args = args.Substring(2).Replace("\"\"", "\"");
                        else
                            args = args.Substring(1).Replace("\\", "");
                        args = args.Substring(0, args.Length - 1);
                        w._(args);
                    }

//                        }
                    w._($"public {propertyType} {propertyName} {{ get; set; }}");
                }
            }
        }
    }
}
