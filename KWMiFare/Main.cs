using Pcsc;
using Pcsc.Common;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Devices.Enumeration;
using Windows.Devices.SmartCards;

namespace KWMiFare
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            Icon = Properties.Resources.baseline_nfc_black_24dp;
            AddToListBox("Written by: Dylan Bickerstaff, 2021.");
            Task.Run(async () => {
                DeviceInformation di = await SmartCardReaderUtils.GetFirstSmartCardReaderInfo(SmartCardReaderKind.Nfc);
                if (di == null)
                {
                    AddToListBox("NFC card reader mode not supported on this device.");
                    return;
                }
                SmartCardReader reader = await SmartCardReader.FromIdAsync(di.Id);
                reader.CardAdded += Reader_CardAdded;
                reader.CardRemoved += Reader_CardRemoved;
            });
        }
        private void Reader_CardRemoved(SmartCardReader sender, CardRemovedEventArgs args)
        {
            AddToListBox("Card removed.");
        }
        private async void Reader_CardAdded(SmartCardReader sender, CardAddedEventArgs args)
        {
            AddToListBox("Card presented.");
            SmartCard sc = args.SmartCard;
            SmartCardConnection scc = await sc.ConnectAsync();
            IccDetection cid = new IccDetection(sc, scc);
            await cid.DetectCardTypeAync();
            AddToListBox("PC/SC device class: " + cid.PcscDeviceClass.ToString());
            AddToListBox("Card name: " + cid.PcscCardName.ToString());
            if (
                cid.PcscDeviceClass == Pcsc.Common.DeviceClass.StorageClass &&
                (cid.PcscCardName == CardName.MifareStandard1K || cid.PcscCardName == CardName.MifareStandard4K)
            ) {
                // Handle MIFARE Standard/Classic
                AddToListBox("MIFARE Standard/Classic card detected");
                var mfStdAccess = new MifareStandard.AccessHandler(scc);
                var uid = await mfStdAccess.GetUidAsync();
                AddToListBox("UID:  " + BitConverter.ToString(uid));
                AddToListBox("UID UINT32: " + BitConverter.ToUInt32(uid, 0));
                SendKeys.SendWait(BitConverter.ToUInt32(uid, 0).ToString());
                /* //Read MIFARE blocks. (Not Needed)
                ushort maxAddress = 0;
                switch (cid.PcscCardName)
                {
                    case Pcsc.CardName.MifareStandard1K:
                        maxAddress = 0x3f;
                        break;
                    case Pcsc.CardName.MifareStandard4K:
                        maxAddress = 0xff;
                        break;
                }
                await mfStdAccess.LoadKeyAsync(MifareStandard.DefaultKeys.FactoryDefault);
                for (ushort address = 0; address <= maxAddress; address++)
                {
                    var response = await mfStdAccess.ReadAsync(address, Pcsc.GeneralAuthenticate.GeneralAuthenticateKeyType.MifareKeyA);
                    AddToListBox("Block " + address.ToString() + " " + BitConverter.ToString(response));
                }
                */
            }
        }
        private void AddToListBox(string text)
        {
            if (MainListBox.InvokeRequired)
            {
                Invoke(new Action(() => {
                    MainListBox.Items.Add(text);
                    MainListBox.SelectedIndex = MainListBox.Items.Count - 1;
                }));
            }
            else
            {
                MainListBox.Items.Add(text);
                MainListBox.SelectedIndex = MainListBox.Items.Count - 1;
            }
        }
    }
}