using System;
using System.ComponentModel;

namespace Michalski.Models
{
    class MakerMock : IMakerModel
    {
        public int id => throw new NotImplementedException();

        public string name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string number { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string address { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
