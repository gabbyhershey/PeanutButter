﻿using System.Collections.Generic;
using System.Collections.Specialized;
using NExpect;
using static NExpect.Expectations;
using NUnit.Framework;
using PeanutButter.DuckTyping.Extensions;
// ReSharper disable MemberCanBePrivate.Global

namespace PeanutButter.DuckTyping.Tests.Extensions
{
    [TestFixture]
    public class TestMergingExtensions
    {
        [TestFixture]
        public class VerifyingMerge
        {
            public interface IMerge1
            {
                int Id { get; }
                string Name { get; }
            }

            [TestFixture]
            public class OperatingOnCollectionsDictionaries
            {
                [Test]
                public void CanMergeAs_OperatingOnCollectionofOneWhichCanBackTheInterface_ShouldReturnTrue()
                {
                    // Arrange
                    var src1 = new Dictionary<string, object>()
                    { 
                        ["Id"] = 1,
                        ["Name"] = "Bob"
                    };

                    // Act
                    var result = new[] { src1 }.CanMergeAs<IMerge1>();
                    // Assert
                    Expect(result).To.Be.True();
                }

                [Test]
                public void CanMergeAs_OperatingOnCollectionofTwoWhichCanBackTheInterface_ShouldReturnTrue()
                {
                    // Arrange
                    var src1 = new Dictionary<string, object>()
                    {
                        ["Id"] = 1
                    };
                    var src2 = new Dictionary<string, object>()
                    {
                        ["Name"] = "Bob"
                    };

                    // Act
                    var result = new[] { src1, src2 }.CanMergeAs<IMerge1>();
                    // Assert
                    Expect(result).To.Be.True();
                }

                [Test]
                public void CanMergeAs_OperatingOnCollectionofTwoWithMoreSpecificTypesWhichCanBackTheInterface_ShouldReturnTrue()
                {
                    // Arrange
                    var src1 = new Dictionary<string, int>()
                    {
                        ["Id"] = 1
                    };
                    var src2 = new Dictionary<string, string>()
                    {
                        ["Name"] = "Bob"
                    };

                    // Act
                    var result = new object[] { src1, src2 }.CanMergeAs<IMerge1>();
                    // Assert
                    Expect(result).To.Be.True();
                }

                [Test]
                public void CanMergeAs_OperatingOnMultiplesIncludingNameValueCollection_WhenShouldPass_Does()
                {
                    // Arrange
                    var src1 = new Dictionary<string, int>()
                    {
                        ["Id"] = 1
                    };
                    var src2 = new NameValueCollection()
                    {
                        { "Name", "Bob" }
                    };

                    // Pre-Assert

                    // Act
                    var result = new object[] { src1, src2 }.CanMergeAs<IMerge1>();

                    // Assert
                    Expect(result).To.Be.True();
                }

                [Test]
                public void CanMergeAs_OperatingOnMultiplesIncludingObject_WhenShouldPass_Does()
                {
                    // Arrange
                    var src1 = new { Id = 1 };
                    var src2 = new NameValueCollection()
                    {
                        { "Name", "Bob" }
                    };

                    // Pre-Assert

                    // Act
                    var result = new object[] { src1, src2 }.CanMergeAs<IMerge1>();

                    // Assert
                    Expect(result).To.Be.True();
                }

            }
        }
    }
}