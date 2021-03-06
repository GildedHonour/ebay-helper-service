﻿// Type: System.IO.TextReader
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.IO
{
    /// <summary>
    /// Represents a reader that can read a sequential series of characters.
    /// </summary>
    /// <filterpriority>2</filterpriority>
    [ComVisible(true)]
    [Serializable]
    public abstract class TextReader : MarshalByRefObject, IDisposable
    {
        /// <summary>
        /// Provides a TextReader with no data to read from.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        public static readonly TextReader Null;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.IO.TextReader"/> class.
        /// </summary>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        protected TextReader();

        /// <summary>
        /// Closes the <see cref="T:System.IO.TextReader"/> and releases any system resources associated with the TextReader.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        public virtual void Close();

        /// <summary>
        /// Releases all resources used by the <see cref="T:System.IO.TextReader"/> object.
        /// </summary>
        public void Dispose();

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.IO.TextReader"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
        protected virtual void Dispose(bool disposing);

        /// <summary>
        /// Reads the next character without changing the state of the reader or the character source. Returns the next available character without actually reading it from the input stream.
        /// </summary>
        /// 
        /// <returns>
        /// An integer representing the next character to be read, or -1 if no more characters are available or the stream does not support seeking.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public virtual int Peek();

        /// <summary>
        /// Reads the next character from the input stream and advances the character position by one character.
        /// </summary>
        /// 
        /// <returns>
        /// The next character from the input stream, or -1 if no more characters are available. The default implementation returns -1.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public virtual int Read();

        /// <summary>
        /// Reads a maximum of <paramref name="count"/> characters from the current stream and writes the data to <paramref name="buffer"/>, beginning at <paramref name="index"/>.
        /// </summary>
        /// 
        /// <returns>
        /// The number of characters that have been read. The number will be less than or equal to <paramref name="count"/>, depending on whether the data is available within the stream. This method returns zero if called when no more characters are left to read.
        /// </returns>
        /// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index"/> and (<paramref name="index"/> + <paramref name="count"/> - 1) replaced by the characters read from the current source. </param><param name="index">The place in <paramref name="buffer"/> at which to begin writing. </param><param name="count">The maximum number of characters to read. If the end of the stream is reached before <paramref name="count"/> of characters is read into <paramref name="buffer"/>, the current method returns. </param><exception cref="T:System.ArgumentNullException"><paramref name="buffer"/> is null. </exception><exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index"/> is less than <paramref name="count"/>. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> or <paramref name="count"/> is negative. </exception><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public virtual int Read([In, Out] char[] buffer, int index, int count);

        /// <summary>
        /// Reads all characters from the current position to the end of the TextReader and returns them as one string.
        /// </summary>
        /// 
        /// <returns>
        /// A string containing all characters from the current position to the end of the TextReader.
        /// </returns>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader"/> is closed. </exception><exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string. </exception><exception cref="T:System.ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="F:System.Int32.MaxValue"/></exception><filterpriority>1</filterpriority>
        public virtual string ReadToEnd();

        /// <summary>
        /// Reads a maximum of <paramref name="count"/> characters from the current stream, and writes the data to <paramref name="buffer"/>, beginning at <paramref name="index"/>.
        /// </summary>
        /// 
        /// <returns>
        /// The position of the underlying stream is advanced by the number of characters that were read into <paramref name="buffer"/>.The number of characters that have been read. The number will be less than or equal to <paramref name="count"/>, depending on whether all input characters have been read.
        /// </returns>
        /// <param name="buffer">When this method returns, this parameter contains the specified character array with the values between <paramref name="index"/> and (<paramref name="index"/> + <paramref name="count"/> -1) replaced by the characters read from the current source. </param><param name="index">The place in <paramref name="buffer"/> at which to begin reading. </param><param name="count">The maximum number of characters to read. </param><exception cref="T:System.ArgumentNullException"><paramref name="buffer"/> is null. </exception><exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index"/> is less than <paramref name="count"/>. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> or <paramref name="count"/> is negative. </exception><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>2</filterpriority>
        public virtual int ReadBlock([In, Out] char[] buffer, int index, int count);

        /// <summary>
        /// Reads a line of characters from the current stream and returns the data as a string.
        /// </summary>
        /// 
        /// <returns>
        /// The next line from the input stream, or null if all characters have been read.
        /// </returns>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception><exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string. </exception><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader"/> is closed. </exception><exception cref="T:System.ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="F:System.Int32.MaxValue"/></exception><filterpriority>1</filterpriority>
        public virtual string ReadLine();

        /// <summary>
        /// Creates a thread-safe wrapper around the specified TextReader.
        /// </summary>
        /// 
        /// <returns>
        /// A thread-safe <see cref="T:System.IO.TextReader"/>.
        /// </returns>
        /// <param name="reader">The TextReader to synchronize. </param><exception cref="T:System.ArgumentNullException"><paramref name="reader"/> is null. </exception><filterpriority>2</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
        public static TextReader Synchronized(TextReader reader);
    }
}
