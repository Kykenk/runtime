// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Xunit;

namespace System.IO.Tests
{
    public class FileStream_ctor_str_fm_fa_fs_buffer : FileStream_ctor_str_fm_fa_fs
    {
        protected const int c_DefaultBufferSize = 4096;

        protected sealed override FileStream CreateFileStream(string path, FileMode mode, FileAccess access, FileShare share)
        {
            return CreateFileStream(path, mode, access, share, c_DefaultBufferSize);
        }

        protected virtual FileStream CreateFileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize)
        {
            return new FileStream(path, mode, access, share, bufferSize);
        }

        [Fact]
        public void NegativeBufferSizeThrows()
        {
            AssertExtensions.Throws<ArgumentOutOfRangeException>(
                GetExpectedParamName("bufferSize"),
                () => CreateFileStream(GetTestFilePath(), FileMode.Create, FileAccess.ReadWrite, FileShare.Read, -1));
        }

        [Fact]
        public void ZeroBufferSizeDoesNotThrow()
        {
            using (CreateFileStream(GetTestFilePath(), FileMode.Create, FileAccess.ReadWrite, FileShare.Read, 0))
            {
            }
        }

        [Fact]
        public void ValidBufferSize()
        {
            using (FileStream fs = CreateFileStream(GetTestFilePath(), FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, 64 * 1024))
            { }
        }
    }
}
