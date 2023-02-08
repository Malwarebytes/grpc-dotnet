#region Copyright notice and license
// Copyright 2019 The gRPC Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Grpc.Core.Api.Utils;

// TODO(jtattermusch): revisit what uses this code and delete once possible.
internal static class EncodingExtensions
{
    /// <summary>
    /// Converts <c>IntPtr</c> pointing to a encoded byte array to a <c>string</c> using the provided <c>Encoding</c>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe string GetString(this Encoding encoding, IntPtr ptr, int len)
    {
#if NET452
        if (len == 0)
        {
            return string.Empty;
        }
        
        // create a local copy in a fresh array
        var arr = new byte[len];
        Marshal.Copy(ptr, arr, 0, len);
        return  encoding.GetString(arr, 0, len);
#else
        return len == 0 ? string.Empty : encoding.GetString((byte*)ptr.ToPointer(), len);
#endif
    }
}

