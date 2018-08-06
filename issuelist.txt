using System;
using System.ComponentModel;
using System.Numerics;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using Helper = Neo.SmartContract.Framework.Helper;

namespace NeoCEContract
{
    public class NeoCE : SmartContract
    {
        // 管理员权限，方便合约迁移
        public static readonly byte[] ContractOwner = "testAddress".ToScriptHash();

        // Cell Evolution - A game about cells & humanity.
        public static string Name() => "CellEvolution";


        delegate object deleDyncall(string method, object[] arr);

    }

    	// Implement with issue list

}
