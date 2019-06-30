﻿using Kerberos.NET.Asn1;
using Kerberos.NET.Asn1.Entities;
using Kerberos.NET.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.Kerberos.NET
{
    [TestClass]
    public class KdcReqTests : BaseTest
    {
        [TestMethod]
        public void TestParseAsReq()
        {
            var asReqBin = ReadDataFile("messages\\as-req");

            var asreq = KrbKdcReq.Decode(asReqBin);

            var bytes = asreq.Encode();

            Assert.IsTrue(asReqBin.SequenceEqual(bytes.ToArray()));
        }

        [TestMethod]
        public void TestParseAsReqWithPaData()
        {
            var asReqBin = ReadDataFile("messages\\as-req-preauth");

            var asreq = KrbKdcReq.Decode(asReqBin);

            var bytes = asreq.Encode();

            Assert.IsTrue(asReqBin.SequenceEqual(bytes.ToArray()));
        }
    }
}