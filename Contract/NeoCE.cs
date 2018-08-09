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

      /**
          * 细胞计数
          */
          private static void _addCellNo(BigInteger count)
            {
               var total = Storage.Get(Storage.CurrentContext, "totalCell").AsBigInteger();
               total += count;
               Storage.Put(Storage.CurrentContext, "totalCell", total);
             }

           public static BigInteger gettotalCharacter()
                 {
                     return Storage.Get(Storage.CurrentContext, "totalCell").AsBigInteger();
                 }
               [Serializable]
               public class CellHistory
               {

                 public string id ;
                 public string cellno ;
                 public string adaption ;
                 public string surviveability;
                 public string division;
                 public string environment;
                 public string day;
                 public string totoalscore;
                 public string worldtitle;
                 public string startcellid;
                 public string endcellid;
                 public string cellsdetail;
                 public string version;

               }
                public class CellEvolution
                              {

                                public string id ;
                                public string cellno ;
                                public string adaption ;
                                public string surviveability;
                                public string division;
                                public string environment;
                                public string day;
                                public string totoalscore;
                                public string finaltitle;
                                public string belong;
                                public string version;

                              }
   // Insert
       public static BigInteger dnamerge(BigInteger id, string[] data)
            {


                //
                if (Runtime.CheckWitness(MintOwner))
                {
                    CellEvolution newCell = new CellEvolution();
                    newCell.id = data[0];
                    newCell.cellno = data[1];
                    newCell.adaption = data[2];
                    newCell.surviveability = data[3];
                    newCell.division = data[4];
                    newCell.environment = data[5];
                    newCharacter.day = data[6];
                    newCell.totoalscore = data[7];
                    newCell.finaltitle = data[8];
                    newCell.belong = data[9];





                    _putCellInfo(id, newCell);

                    Runtime.Notify("newCellGenerated", id);
                    return id;
                }
                else
                {
                    Runtime.Log("Only the contract owner may mint new tokens.");
                    return 0;
                }
            }


  // Get Cell
  private static object[] getCellInfo(BigInteger id)
        {
            var key = id;
            var bytes = Storage.Get(Storage.CurrentContext, key);
            if (bytes.Length == 0)
                return new object[0];
            return (object[]) bytes.Deserialize();
        }

        // Add Cell
       private static void _putCellInfo(BigInteger id, CellEvolution info)
             {
                 var key = id
                 byte[] nftInfo = Helper.Serialize(info);

                 Storage.Put(Storage.CurrentContext, key, nftInfo);
             }



        public static Object Main(string operation, params object[] args)
        {
            if (Runtime.Trigger == TriggerType.Verification)
            {
                if (ContractOwner.Length == 20)
                {
                    // if param ContractOwner is script hash
                    //return Runtime.CheckWitness(ContractOwner);
                    return false;
                }

                if (ContractOwner.Length == 33)
                {

                    byte[] signature = operation.AsByteArray();
                    return VerifySignature(signature, ContractOwner);
                }

                return false;
            }

            if (Runtime.Trigger == TriggerType.VerificationR)
            {
                return true;
            }

            if (Runtime.Trigger == TriggerType.Application)
            {

                var callscript = ExecutionEngine.CallingScriptHash;
                if (operation == "name") return Name();
                if (operation == "decimals") return 0; // NFT can't divide, decimals allways zero

                if (operation == "hasExtraData") return false;
                if (operation == "isEnumable") return false;
                if (operation == "hasBroker") return false;



                if (operation == "dnamerge")
                {
                    if (args.Length != 2)
                        return false;

                    var id = (BigInteger) args[0];
                    var data = (string) args[1];

                    return dnamerge( id, data);
                }



            }

            return false;
        }

}
