﻿using Cam.Wallets;

namespace Cam.UI
{
    internal class TxOutListBoxItem : TransferOutput
    {
        public string AssetName;

        public override string ToString()
        {
            return $"{ScriptHash.ToAddress()}\t{Value}\t{AssetName}";
        }
    }
}
