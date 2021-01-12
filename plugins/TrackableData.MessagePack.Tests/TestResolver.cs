// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using TrackableData.MessagePack;

public class TestResolver : IFormatterResolver
{
    public static MessagePackSerializerOptions GetMessagePackOption()
    {
        return MessagePackSerializerOptions.Standard.WithResolver(new TestResolver(StandardResolver.Instance,
            new TrackableDataMessagePacketResolver()));
    }

    private static IReadOnlyList<IFormatterResolver> resolvers;

    public TestResolver(params IFormatterResolver[] resolvers)
    {
        TestResolver.resolvers = resolvers;
    }

    public IMessagePackFormatter<T> GetFormatter<T>()
    {
        return Cache<T>.Formatter;
    }

    private static class Cache<T>
    {
        public static readonly IMessagePackFormatter<T> Formatter;

        static Cache()
        {
            foreach (var item in resolvers)
            {
                var f = item.GetFormatter<T>();
                if (f != null)
                {
                    Formatter = f;
                    return;
                }
            }
        }
    }
}
