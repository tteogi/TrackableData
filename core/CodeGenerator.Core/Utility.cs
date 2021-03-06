﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeGen
{
    public class ClassSerializeType
    {
        public interface ISerializeType
        {
            string TypeString();
        }

        public class MessagePack : ISerializeType
        {
            public string TypeString()
            {
                return "[MessagePack.MessagePackObject]";
            }
        }

        public class Protobuf : ISerializeType
        {
            public string TypeString()
            {
                return "[ProtoContract]";
            }
        }

        private List<ISerializeType> _serializes;

        public ClassSerializeType(List<ISerializeType> serializes)
        {
            _serializes = serializes;
        }

        public void WriteSerializerString(CodeWriter.CodeWriter writer)
        {
            foreach (var serializeType in _serializes)
            {
                writer.Write(serializeType.TypeString());
            }
        }

        public void WriteIgnore(CodeWriter.CodeWriter writer)
        {
            var serializeType = _serializes.Find((type => type is MessagePack));
            if (serializeType != null)
                writer.Write("[MessagePack.IgnoreMember]");
            writer.Write("[IgnoreDataMember]");
        }
    }


    public static class Utility
    {
        public static bool IsTrackableType(TypeSyntax type)
        {
            // NOTE: it's naive approach because we don't know semantic type information here.
            var parts = type.ToString().Split('.');
            var typeName = parts[parts.Length - 1];
            return typeName.StartsWith("Trackable");
        }

        public static PropertyDeclarationSyntax[] GetTrackableProperties(PropertyDeclarationSyntax[] properties)
        {
            // NOTE: it's naive approach because we don't know semantic type information here.
            return properties.Where(p => IsTrackableType(p.Type)).ToArray();
        }

        public static string GetTrackerClassName(TypeSyntax type)
        {
            // NOTE: it's naive approach because we don't know semantic type information here.
            var genericType = type as GenericNameSyntax;
            if (genericType == null)
            {
                if (type.ToString().StartsWith("Trackable"))
                {
                    return $"TrackablePocoTracker<I{type.ToString().Substring(9)}>";
                }
            }
            else if (CodeAnalaysisExtensions.CompareTypeName(genericType.Identifier.ToString(),
                "TrackableData.TrackableDictionary"))
            {
                return $"TrackableDictionaryTracker{genericType.TypeArgumentList}";
            }
            else if (CodeAnalaysisExtensions.CompareTypeName(genericType.Identifier.ToString(),
                "TrackableData.TrackableSet"))
            {
                return $"TrackableSetTracker{genericType.TypeArgumentList}";
            }
            else if (CodeAnalaysisExtensions.CompareTypeName(genericType.Identifier.ToString(),
                "TrackableData.TrackableList"))
            {
                return $"TrackableListTracker{genericType.TypeArgumentList}";
            }

            throw new Exception("Cannot resolve tracker class of " + type);
        }

        public static ClassSerializeType GetClassSerializeType(SyntaxList<AttributeListSyntax> attributes)
        {
            List<ClassSerializeType.ISerializeType> serializes = new List<ClassSerializeType.ISerializeType>();
            var useProtoContract = attributes.GetAttribute("ProtoContractAttribute") != null;
            if (useProtoContract)
                serializes.Add(new ClassSerializeType.Protobuf());

            var useMessagePack = attributes.GetAttribute("UnionAttribute") != null;
            if (useMessagePack)
                serializes.Add(new ClassSerializeType.MessagePack());
            return new ClassSerializeType(serializes);
        }

        public static bool IsMessagePackObject(SyntaxList<AttributeListSyntax> attributes)
        {
            return attributes.GetAttribute("UnionAttribute") != null;
        }

        public static string GetMessagePackAot(TypeSyntax type)
        {
            // NOTE: it's naive approach because we don't know semantic type information here.
            var genericType = type as GenericNameSyntax;
            if (genericType == null)
            {
                if (type.ToString().StartsWith("Trackable"))
                {
                    return $"new TrackableData.MessagePack.TrackablePocoTrackerClassMessagePackFormatter<I{type.ToString().Substring(9)}>()," +
                           $"new TrackableData.MessagePack.TrackablePocoTrackerInterfaceMessagePackFormatter<I{type.ToString().Substring(9)}>(),";
                }
            }
            else if (CodeAnalaysisExtensions.CompareTypeName(genericType.Identifier.ToString(),
                "TrackableData.TrackableDictionary"))
            {
                return $"new TrackableData.MessagePack.TrackableDictionaryTrackerInterfaceMessagePackFormatter{genericType.TypeArgumentList}()," +
                       $"new TrackableData.MessagePack.TrackableDictionaryTrackerClassMessagePackFormatter{genericType.TypeArgumentList}()," +
                       $"new TrackableData.MessagePack.TrackableDictionaryMessagePackFormatter{genericType.TypeArgumentList}(),";
            }
            else if (CodeAnalaysisExtensions.CompareTypeName(genericType.Identifier.ToString(),
                "TrackableData.TrackableSet"))
            {
                return $"new TrackableData.MessagePack.TrackableSetTrackerInterfaceMessagePackFormatter{genericType.TypeArgumentList}()," +
                       $"new TrackableData.MessagePack.TrackableSetTrackerClassMessagePackFormatter{genericType.TypeArgumentList}()," +
                       $"new TrackableData.MessagePack.TrackableSetMessagePackFormatter{genericType.TypeArgumentList}(),";
            }
            else if (CodeAnalaysisExtensions.CompareTypeName(genericType.Identifier.ToString(),
                "TrackableData.TrackableList"))
            {
                return $"new TrackableData.MessagePack.TrackableListTrackerInterfaceMessagePackFormatter{genericType.TypeArgumentList}()," +
                       $"new TrackableData.MessagePack.TrackableListTrackerClassMessagePackFormatter{genericType.TypeArgumentList}()," +
                       $"new TrackableData.MessagePack.TrackableListMessagePackFormatter{genericType.TypeArgumentList}(),\n";
            }

            throw new Exception("Cannot resolve tracker class of " + type);
        }
    }
}
