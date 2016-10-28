﻿using System;
using System.Reflection;
using NUnit.Framework;
using PeanutButter.DuckTyping.AutoConversion;
using PeanutButter.DuckTyping.AutoConversion.Converters;
using PeanutButter.TestUtils.Generic;
using PeanutButter.Utils;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace PeanutButter.DuckTyping.Tests.AutoConversion.Converters
{
    [TestFixture]
    public class TestGenericStringConverter: AssertionHelper
    {
        [Test]
        public void ShouldImplement_IConverterOf_StringAndT()
        {
            //--------------- Arrange -------------------
            var instance = Create<int>();
            var type = instance.GetType();

            //--------------- Assume ----------------

            //--------------- Act ----------------------
            type.ShouldImplement<IConverter<string, int>>();

            //--------------- Assert -----------------------
        }

        [TestCase(typeof(int))]
        [TestCase(typeof(Guid))]
        public void ShouldSetTypePropertiesAppropriately(Type t)
        {
            //--------------- Arrange -------------------
            var instance = CreateForType(t);

            //--------------- Assume ----------------

            //--------------- Act ----------------------
            var t1 = instance.GetPropertyValue("T1");
            var t2 = instance.GetPropertyValue("T2");

            //--------------- Assert -----------------------
            Expect(t1, Is.EqualTo(typeof(string)));
            Expect(t2, Is.EqualTo(t));
        }


        [TestCase(typeof(int))]
        [TestCase(typeof(long))]
        [TestCase(typeof(short))]
        [TestCase(typeof(Guid))]
        public void ShouldBeAbleToConvertExpectedType_(Type type)
        {
            //--------------- Arrange -------------------
            var method = GetType()
                .GetMethod("Test", BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(type);

            //--------------- Assume ----------------

            //--------------- Act ----------------------
            method.Invoke(this, new object[0]);

            //--------------- Assert -----------------------
        }

        [Test]
        public void ShouldBeAbleToConvertDateTime_CloseEnough()
        {
            //--------------- Arrange -------------------
            var sut = Create<DateTime>();
            var input1 = GetRandom<DateTime>().TruncateMilliseconds();
            var expected1 = input1.ToString();
            var expected2 = GetRandom<DateTime>().TruncateMilliseconds();
            var input2 = expected2.ToString();

            //--------------- Assume ----------------

            //--------------- Act ----------------------
            var result1 = sut.Convert(input1);
            var result2 = sut.Convert(input2);

            //--------------- Assert -----------------------
            Expect(result1, Is.EqualTo(expected1));
            Expect(result2, Is.EqualTo(expected2));
        }

        [Test]
        public void ShouldBeAbleToConvertFloats_CloseEnough()
        {
            //--------------- Arrange -------------------
            var sut = Create<float>();
            var input1 = (float)GetRandomInt(1, 10);
            var expected1 = input1.ToString();
            var expected2 = (float)GetRandomInt(1, 10);
            var input2 = expected2.ToString();

            //--------------- Assume ----------------

            //--------------- Act ----------------------
            var result1 = sut.Convert(input1);
            var result2 = sut.Convert(input2);

            //--------------- Assert -----------------------
            Expect(result1, Is.EqualTo(expected1));
            Expect(result2, Is.EqualTo(expected2));
        }




        // ReSharper disable once UnusedMember.Local
#pragma warning disable S1144 // Unused private types or members should be removed
        private void Test<T>()
        {
            //--------------- Arrange -------------------
            var sut = Create<T>();
            var input1 = GetRandom<T>();
            var expected1 = input1.ToString();
            var expected2 = GetRandom<T>();
            var input2 = expected2.ToString();

            //--------------- Assume ----------------

            //--------------- Act ----------------------
            var result1 = sut.Convert(input1);
            var result2 = sut.Convert(input2);


            //--------------- Assert -----------------------
            Expect(result1, Is.EqualTo(expected1));
            Expect(result2, Is.EqualTo(expected2));
        }
#pragma warning restore S1144 // Unused private types or members should be removed

        private IConverter<string, T> Create<T>()
        {
            return new GenericStringConverter<T>();
        }

        private IConverter CreateForType(Type t)
        {
            var method = GetType().GetMethod("Create", BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(t);
            return (IConverter)method.Invoke(this, new object[0]);
        }
    }
}