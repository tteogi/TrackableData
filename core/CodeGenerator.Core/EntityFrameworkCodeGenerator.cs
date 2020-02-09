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

        public void GenerateCode(TypeDeclarationSyntax idecl, CodeWriter.CodeWriter w)
        {
            var iname = idecl.Identifier.ToString();
            Console.WriteLine("GenerateCode: " + iname);

            w._($"#region {iname}DbContext");
            w._();

            var namespaceHandle = w.B($"namespace Database");

            GenerateDbContextCode(idecl, w);

            namespaceHandle?.Dispose();

            w._();
            w._($"#endregion");
        }

        private void GenerateDbContextCode(TypeDeclarationSyntax idecl, CodeWriter.CodeWriter w)
        {
            var typeName = idecl.GetTypeName();
            string className;
            if (idecl is ClassDeclarationSyntax)
            {
                className = typeName;
            }
            else
                className = typeName.Substring(1);

            var hideProperties = new List<Tuple<string, string, string>>();
            var attribute = idecl.AttributeLists.GetAttribute("EntityFrameworkModel");
            foreach (var arg in attribute.ArgumentList.Arguments)
            {
                var ss = arg.ToString();
                var span = ss.Split(':');
                if (span.Length == 3)
                {
                    hideProperties.Add(new Tuple<string, string, string>(span[1], span[2].Remove(span[2].Length - 1), null));
                }
                if (span.Length == 4)
                {
                    hideProperties.Add(new Tuple<string, string, string>(span[1], span[2], "\""+ span[3]));
                }
            }

            var properties = idecl.GetProperties();

            using (w.b($"public partial class {className}"))
            {
                void SetEntityFrameworkAttribute(string args)
                {
                    if (args.StartsWith("@"))
                        args = args.Substring(2).Replace("\"\"", "\"");
                    else
                        args = args.Substring(1).Replace("\\", "");
                    args = args.Substring(0, args.Length - 1);
                    w._(args);
                }

                foreach (var property in hideProperties)
                {
                    if (property.Item3 != null)
                    {
                        SetEntityFrameworkAttribute(property.Item3);
                    }
                    w._($"public {property.Item1} {property.Item2} {{ get; set; }}");
                }

                // Property Accessors
                foreach (var p in properties)
                {
                    var propertyType = p.Type.ToString();
                    var propertyName = p.Identifier.ToString();
                    var ignore = p.AttributeLists.GetAttribute("IgnoreEntityFrameworkModel");
                    if (ignore != null)
                        continue;
                    var entityFrameworkAttribute = p.AttributeLists.GetAttribute("EntityFrameworkPropertyAttribute");
                    if (entityFrameworkAttribute != null)
                    {
                        var args = entityFrameworkAttribute.ArgumentList.Arguments.ToString();
                        SetEntityFrameworkAttribute(args);
                    }
                    w._($"public {propertyType} {propertyName} {{ get; set; }}");
                }
            }
        }
    }
}
