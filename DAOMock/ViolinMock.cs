using System;
using System.ComponentModel;

namespace Michalski.Models
{
    public class ViolinMock : IViolinModel
    {
        public int id => throw new NotImplementedException();

        public string name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string maker { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint year { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint price { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ViolinState state { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
