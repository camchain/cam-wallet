using Cam.Core;
using Cam.Properties;
using Cam.SmartContract;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.Text;
using System.Numerics;

namespace Cam.UI
{
    internal static class Helper
    {
        private static Dictionary<Type, Form> tool_forms = new Dictionary<Type, Form>();

        private static void Helper_FormClosing(object sender, FormClosingEventArgs e)
        {
            tool_forms.Remove(sender.GetType());
        }

        public static void Show<T>() where T : Form, new()
        {
            Type t = typeof(T);
            if (!tool_forms.ContainsKey(t))
            {
                tool_forms.Add(t, new T());
                tool_forms[t].FormClosing += Helper_FormClosing;
            }
            tool_forms[t].Show();
            tool_forms[t].Activate();
        }

        public static void SignAndShowInformation(Transaction tx)
        {
            if (tx == null)
            {
                MessageBox.Show(Strings.InsufficientFunds);
                return;
            }
            ContractParametersContext context;
            try
            {
                context = new ContractParametersContext(tx);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show(Strings.UnsynchronizedBlock);
                return;
            }
            Program.CurrentWallet.Sign(context);
            if (context.Completed)
            {
                context.Verifiable.Scripts = context.GetScripts();
                Program.CurrentWallet.ApplyTransaction(tx);
                Program.LocalNode.Relay(tx);
                InformationBox.Show(tx.Hash.ToString(), Strings.SendTxSucceedMessage, Strings.SendTxSucceedTitle);
            }
            else
            {
                InformationBox.Show(context.ToString(), Strings.IncompletedSignatureMessage, Strings.IncompletedSignatureTitle);
            }
        }





        public static string ToOpCodeFormat(string hexString)
        {
            StringBuilder sb = new StringBuilder();

            int maxCount = 100;

            byte[] script = Cam.Helper.HexToBytes(hexString);
            int nextIndex = 0;
            while (nextIndex < script.Length && maxCount > 0)
            {
                byte opCodeByte = script[nextIndex];
                Cam.VM.OpCode opCode = (Cam.VM.OpCode)opCodeByte;

                string opMsg = PreExecuteOp(opCode, nextIndex, script, out nextIndex);

                sb.Append(opMsg);

                maxCount--;//每执行一次opCode，减少1次计数
            }

            return sb.ToString();
        }







        private static string PreExecuteOp(VM.OpCode opcode, int index, byte[] script, out int nextIndex)
        {
            
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(opcode.ToString());

            nextIndex = index + 1;

            if (opcode >= VM.OpCode.PUSHBYTES1 && opcode <= VM.OpCode.PUSHBYTES75)
            {
                int len = (byte)opcode;

                nextIndex = index + len + 1;

                byte[] data = new byte[len];
                Array.Copy(script, index + 1, data, 0, len);//拷贝的是opcode命令后面的数据
                sb.AppendLine(Cam.Helper.ToHexString(data));
            }
            else
            {
                switch (opcode)
                {

                    case VM.OpCode.PUSH0:

                        break;
                    case VM.OpCode.PUSHDATA1:

                        break;
                    case VM.OpCode.PUSHDATA2:

                        break;
                    case VM.OpCode.PUSHDATA4:

                        break;
                    case VM.OpCode.PUSHM1:
                    case VM.OpCode.PUSH1:
                    case VM.OpCode.PUSH2:
                    case VM.OpCode.PUSH3:
                    case VM.OpCode.PUSH4:
                    case VM.OpCode.PUSH5:
                    case VM.OpCode.PUSH6:
                    case VM.OpCode.PUSH7:
                    case VM.OpCode.PUSH8:
                    case VM.OpCode.PUSH9:
                    case VM.OpCode.PUSH10:
                    case VM.OpCode.PUSH11:
                    case VM.OpCode.PUSH12:
                    case VM.OpCode.PUSH13:
                    case VM.OpCode.PUSH14:
                    case VM.OpCode.PUSH15:
                    case VM.OpCode.PUSH16:

                        break;

                    case VM.OpCode.NOP:
                        break;
                    case VM.OpCode.JMP:
                    case VM.OpCode.JMPIF:
                    case VM.OpCode.JMPIFNOT:
                        {
















                        }
                        break;
                    case VM.OpCode.CALL:



                        break;
                    case VM.OpCode.RET:



                        break;
                    case VM.OpCode.APPCALL:
                    case VM.OpCode.TAILCALL:
                        {



















                        }
                        break;
                    case VM.OpCode.SYSCALL:


                        break;

                    case VM.OpCode.DUPFROMALTSTACK:

                        break;
                    case VM.OpCode.TOALTSTACK:

                        break;
                    case VM.OpCode.FROMALTSTACK:

                        break;
                    case VM.OpCode.XDROP:
                        {







                        }
                        break;
                    case VM.OpCode.XSWAP:
                        {










                        }
                        break;
                    case VM.OpCode.XTUCK:
                        {







                        }
                        break;
                    case VM.OpCode.DEPTH:

                        break;
                    case VM.OpCode.DROP:

                        break;
                    case VM.OpCode.DUP:

                        break;
                    case VM.OpCode.NIP:
                        {



                        }
                        break;
                    case VM.OpCode.OVER:
                        {




                        }
                        break;
                    case VM.OpCode.PICK:
                        {







                        }
                        break;
                    case VM.OpCode.ROLL:
                        {








                        }
                        break;
                    case VM.OpCode.ROT:
                        {






                        }
                        break;
                    case VM.OpCode.SWAP:
                        {




                        }
                        break;
                    case VM.OpCode.TUCK:
                        {





                        }
                        break;
                    case VM.OpCode.CAT:
                        {



                        }
                        break;
                    case VM.OpCode.SUBSTR:
                        {














                        }
                        break;
                    case VM.OpCode.LEFT:
                        {








                        }
                        break;
                    case VM.OpCode.RIGHT:
                        {













                        }
                        break;
                    case VM.OpCode.SIZE:
                        {


                        }
                        break;

                    case VM.OpCode.INVERT:
                        {


                        }
                        break;
                    case VM.OpCode.AND:
                        {



                        }
                        break;
                    case VM.OpCode.OR:
                        {



                        }
                        break;
                    case VM.OpCode.XOR:
                        {



                        }
                        break;
                    case VM.OpCode.EQUAL:
                        {



                        }
                        break;

                    case VM.OpCode.INC:
                        {


                        }
                        break;
                    case VM.OpCode.DEC:
                        {


                        }
                        break;
                    case VM.OpCode.SIGN:
                        {


                        }
                        break;
                    case VM.OpCode.NEGATE:
                        {


                        }
                        break;
                    case VM.OpCode.ABS:
                        {


                        }
                        break;
                    case VM.OpCode.NOT:
                        {


                        }
                        break;
                    case VM.OpCode.NZ:
                        {


                        }
                        break;
                    case VM.OpCode.ADD:
                        {



                        }
                        break;
                    case VM.OpCode.SUB:
                        {



                        }
                        break;
                    case VM.OpCode.MUL:
                        {



                        }
                        break;
                    case VM.OpCode.DIV:
                        {



                        }
                        break;
                    case VM.OpCode.MOD:
                        {



                        }
                        break;
                    case VM.OpCode.SHL:
                        {



                        }
                        break;
                    case VM.OpCode.SHR:
                        {



                        }
                        break;
                    case VM.OpCode.BOOLAND:
                        {



                        }
                        break;
                    case VM.OpCode.BOOLOR:
                        {



                        }
                        break;
                    case VM.OpCode.NUMEQUAL:
                        {



                        }
                        break;
                    case VM.OpCode.NUMNOTEQUAL:
                        {



                        }
                        break;
                    case VM.OpCode.LT:
                        {



                        }
                        break;
                    case VM.OpCode.GT:
                        {



                        }
                        break;
                    case VM.OpCode.LTE:
                        {



                        }
                        break;
                    case VM.OpCode.GTE:
                        {



                        }
                        break;
                    case VM.OpCode.MIN:
                        {



                        }
                        break;
                    case VM.OpCode.MAX:
                        {



                        }
                        break;
                    case VM.OpCode.WITHIN:
                        {




                        }
                        break;

                    case VM.OpCode.SHA1:





                        break;
                    case VM.OpCode.SHA256:





                        break;
                    case VM.OpCode.HASH160:
                        {


                        }
                        break;
                    case VM.OpCode.HASH256:
                        {


                        }
                        break;
                    case VM.OpCode.CHECKSIG:
                        {











                            sb.AppendLine("single signature contract");
                        }
                        break;
                    case VM.OpCode.CHECKMULTISIG:
                        {

                            sb.AppendLine("multi signature contract");




































































                        }
                        break;

                    case VM.OpCode.ARRAYSIZE:
                        {





                        }
                        break;
                    case VM.OpCode.PACK:
                        {










                        }
                        break;
                    case VM.OpCode.UNPACK:
                        {










                        }
                        break;
                    case VM.OpCode.PICKITEM:
                        {



















                        }
                        break;
                    case VM.OpCode.SETITEM:
                        {



















                        }
                        break;
                    case VM.OpCode.NEWARRAY:
                        {







                        }
                        break;
                    case VM.OpCode.NEWSTRUCT:
                        {







                        }
                        break;
                    case VM.OpCode.APPEND:
                        {













                        }
                        break;

                    case VM.OpCode.REVERSE:
                        {







                        }
                        break;
                    case VM.OpCode.LOCK:
                        {

























                            byte[] timestampByteAr = new byte[4];
                            Array.Copy(script, index - 4, timestampByteAr, 0, 4);
                            BigInteger timestamp = new BigInteger(timestampByteAr);
                            DateTime dateTime = Cam.Helper.ToDateTime((uint)timestamp);
                            sb.AppendLine("lock time:" + dateTime.ToString());
                            sb.AppendLine("lock contract");

                        }
                        break;

                    case VM.OpCode.THROW:

                        break;
                    case VM.OpCode.THROWIFNOT:





                        break;

                    default:

                        break;
                }
            }

            return sb.ToString();

        }
    }
}
