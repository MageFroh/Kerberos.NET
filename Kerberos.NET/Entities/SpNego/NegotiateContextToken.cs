﻿using Kerberos.NET.Asn1.Entities;
using Kerberos.NET.Crypto;
using System;

namespace Kerberos.NET.Entities
{
    public sealed class NegotiateContextToken : ContextToken
    {
        private readonly NegotiationToken token;

        public NegotiateContextToken(GssApiToken? gssToken = null)
        {
            // SPNego tokens optimistically include a token of the first MechType
            // so if mechType[0] == Ntlm process as ntlm, == kerb process as kerb, etc.

            token = NegotiationToken.Decode(gssToken.Value.Field1.Value);
        }

        public override DecryptedKrbApReq DecryptApReq(KeyTable keys)
        {
            var mechToken = token.InitialToken.Value.MechToken.Value;

            var apReq = MessageParser.Parse<ContextToken>(mechToken.ToArray());

            if (apReq is NegotiateContextToken)
            {
                throw new InvalidOperationException(
                    "Negotiated ContextToken is another negotiated token. Failing to prevent stack overflow."
                );
            }

            return apReq.DecryptApReq(keys);
        }
    }
}