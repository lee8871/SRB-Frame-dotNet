using System;
using System.Collections;
using System.Collections.Generic;

namespace SRB_Chart
{
    class StepResizeable<T>: IEnumerable<T>
    {
        int ps;//piece_size
        object[] top_array;
        int length = 0;
        public int Length { get => length; }
        public StepResizeable(int piece_size= 1024)
        {
            this.ps = piece_size;
            top_array = new object[piece_size];
        }
        public void append(T value)
        {
            if (top_array[length / ps] == null)
            {
                top_array[length / ps] = new T[ps];
            }
            (top_array[length / ps] as T[])[length % ps] = value;
            length++;
        }

        public T this[int i]
        {
            get
            {
                if(i>=length)
                {
                    throw new OverflowException();
                }
                return (top_array[i / ps] as T[])[i % ps];
            }
            set
            {
                if(i>=length)
                {
                    throw new OverflowException();
                }
                (top_array[i / ps] as T[])[i % ps] = value;
            }

        }
        class Enumerator : IEnumerator<T>
        {
            StepResizeable<T> ts;
            int i = 0;
            public Enumerator(StepResizeable<T> ts)
            {
                this.ts = ts;
            }

            public T Current => ts[i];

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
            public bool MoveNext()
            {
                i++;
                return (i < ts.length);
            }
            public void Reset()
            {
                i = 0;
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }
    }
}
