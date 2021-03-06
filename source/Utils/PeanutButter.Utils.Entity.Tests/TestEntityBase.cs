﻿using NUnit.Framework;
// ReSharper disable MemberCanBePrivate.Global

namespace PeanutButter.Utils.Entity.Tests
{
    [TestFixture]
    public class TestEntityBase
    {
        public class SomeDerivative : EntityBase
        {
        }

        [Test]
        public void EntitiesWhichInheritFromEntityBase_ShouldStartLifeEnabled()
        {
            //---------------Set up test pack-------------------
            var sut = new SomeDerivative();

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = sut.Enabled;

            //---------------Test Result -----------------------
            Assert.IsTrue(result);
        }

    }
}
