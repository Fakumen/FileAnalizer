using System;

namespace FileSystemAnalizer.App
{
    public abstract class Scanner<TInData, TOutData>
    {
        protected readonly Func<TInData, bool> inputDataIsValid;

        public Scanner(Func<TInData, bool> inputDataIsValid)
        {
            this.inputDataIsValid = inputDataIsValid;
        }

        public TOutData TryScan(TInData data)
        {
            if (!inputDataIsValid(data))
                throw new ArgumentException();
            return Scan(data);
        }

        protected abstract TOutData Scan(TInData data);
    }
}