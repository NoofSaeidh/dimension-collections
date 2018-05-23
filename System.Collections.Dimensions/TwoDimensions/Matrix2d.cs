// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Dimensions.TwoDimensions
{
    // todo: exception messages
    [Serializable]
    public class Matrix2d<T> : IMatrix2d<T>, IReadOnlyMatrix2d<T>, IReadOnlyMatrixXd<T>
    {
        // from mscorlib Array
        internal const int MaxArrayLength = 0X7FEFFFFF;

        private const int _defaultCapacity = 4;

        private static readonly T[,] _emptyArray = new T[0, 0];

        private int _capacityX;

        private int _capacityY;

        private int _countTotal;

        private int _countX;

        private int _countY;

        private T[,] _items;

        [NonSerialized]
        private object _syncRoot;

        private int _version;

        public Matrix2d()
        {
            _items = _emptyArray;
        }

        public Matrix2d(int capacityX, int capacityY)
        {
            if (capacityX < 0 || capacityY < 0)
                throw new ArgumentOutOfRangeException();

            _items = new T[capacityX, capacityY];
            _capacityX = capacityX;
            _capacityY = capacityY;
        }

        public Matrix2d(Index2d capacities) : this(capacities.X, capacities.Y)
        {
        }

        public Matrix2d(Index2d counts, IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (counts.X < 0 || counts.Y < 0)
                throw new ArgumentOutOfRangeException();
            var array = InputAsArray(items);
            if (array.Length == 0)
            {
                _items = _emptyArray;
                return;
            }
            if (array.Length > counts.X * counts.Y)
                throw new ArgumentException();
            _items = new T[counts.X, counts.Y];
            _countX = _capacityX = counts.X;
            _countY = _capacityY = counts.Y;
            CopyArrayToItems(array);
        }

        // throw, skip or add default if new collection that added
        // has less values that curretn dimension
        public DimensionChangeBehavior AddingFewerItemsBehavior { get; set; }

        // the same but extend all other values (if FillDefault specified)
        // if new value has more values
        public DimensionChangeBehavior AddingLargerItemsBehavior { get; set; }

        public Index2d Capacities
        {
            get
            {
                return new Index2d(_capacityX, _capacityY);
            }
            set
            {
                // todo: methods
                if (value.X < _countX)
                {
                    throw new ArgumentOutOfRangeException();
                }
                if (value.Y < _countY)
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (value.X != _items.GetLength(0)
                    || value.Y != _items.GetLength(1))
                {
                    if (value.X > 0 || value.Y > 0)
                    {
                        T[,] newItems = new T[value.X, value.Y];
                        if (_countTotal > 0)
                        {
                            Array.Copy(_items, 0, newItems, 0, _countTotal);
                        }
                        _items = newItems;
                    }
                    else
                    {
                        _items = _emptyArray;
                    }

                    _capacityX = _items.GetLength(0);
                    _capacityY = _items.GetLength(1);
                }
            }
        }

        public int CapacityTotal => _items.Length;

        public int CapacityX
        {
            get
            {
                return _capacityX;
            }
            set
            {
                // todo: methods
                if (value < _countX)
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (value != _capacityX)
                {
                    if (value > 0)
                    {
                        T[,] newItems = new T[value, _capacityY];
                        if (_countTotal > 0)
                        {
                            Array.Copy(_items, 0, newItems, 0, _countTotal);
                        }
                        _items = newItems;
                    }
                    else
                    {
                        _items = _emptyArray;
                    }

                    _capacityX = _items.GetLength(0);
                }
            }
        }

        public int CapacityY
        {
            get
            {
                return _capacityY;
            }
            set
            {
                // todo: methods
                if (value < _countY)
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (value != _capacityY)
                {
                    if (value > 0)
                    {
                        T[,] newItems = new T[_capacityX, value];
                        if (_countTotal > 0)
                        {
                            Array.Copy(_items, 0, newItems, 0, _countTotal);
                        }
                        _items = newItems;
                    }
                    else
                    {
                        _items = _emptyArray;
                    }

                    _capacityY = _items.GetLength(1);
                }
            }
        }

        public int CountX
        {
            get
            {
                return _countX;
            }
            private set
            {
                _countX = value;
                _countTotal = _countX * _countY;
            }
        }

        public int CountY
        {
            get
            {
                return _countY;
            }
            private set
            {
                _countY = value;
                _countTotal = _countX * _countY;
            }
        }


        public bool IsFixedSize => false;

        public int Count => _countTotal;

        public Index2d Counts => new Index2d(CountX, CountY);

        IIndexXd IReadOnlyCollectionXd<T>.Counts => Counts;

        IIndexXd ICollectionXd<T>.Counts => Counts;

        //todo: sync
        bool ICollectionXd<T>.IsReadOnly => false;

        bool ICollectionXd<T>.IsSynchronized => false;

        public int Rank => Index2d.Rank2d;

        object ICollectionXd<T>.SyncRoot
        {
            get
            {
                if (_syncRoot == null)
                {
                    System.Threading.Interlocked.CompareExchange<object>(ref _syncRoot, new object(), null);
                }
                return _syncRoot;
            }
        }

        public T this[Index2d index]
        {
            get => this[index.X, index.Y];
            set => this[index.X, index.Y] = value;
        }

        public T this[int x, int y]
        {
            get
            {
                //todo: method?
                // Following trick can reduce the range check by one
                if (x < 0 || y < 0
                    || (uint)x >= (uint)_countX
                    || (uint)y >= (uint)_countY)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return _items[x, y];
            }

            set
            {
                if (x < 0 || y < 0
                    || (uint)x >= (uint)_countX
                    || (uint)y >= (uint)_countY)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _items[x, y] = value;
                _version++;
            }
        }

        T IMatrixXd<T>.this[IIndexXd index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        T IReadOnlyMatrixXd<T>.this[IIndexXd index] => ((IMatrixXd<T>)this)[index];

        public void AddX(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            var list = EnsureListSizes(items, _countY, out var newCountY);
            if (list == null) return;

            CountY = newCountY;
            if (_countY >= _capacityY)
                EnsureCapacityY(_countY);

            if (_countX == _capacityX)
                EnsureCapacityX(_countX + 1);

            for (int i = 0; i < list.Count; i++)
            {
                _items[_countX, i] = list[i];
            }

            _version++;
        }

        public void AddY(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            var list = EnsureListSizes(items, _countX, out var newCountX);
            if (list == null) return;

            CountX = newCountX;
            if (_countX >= _capacityX)
                EnsureCapacityY(_countX);

            if (_countY == _capacityY)
                EnsureCapacityX(_countY + 1);

            for (int i = 0; i < list.Count; i++)
            {
                _items[i, _countY] = list[i];
            }

            _version++;
        }

        public void Clear()
        {
            if (_countTotal > 0)
            {
                Array.Clear(_items, 0, _countTotal);
                _countX = _countY = _countTotal = 0;
            }
            _version++;
        }

        public bool Contains(T item)
        {
            if ((object)item == null)
            {
                for (int i = 0; i < _countX; i++)
                    for (int j = 0; j < _countY; j++)
                        if ((object)_items[i, j] == null)
                            return true;
                return false;
            }
            else
            {
                EqualityComparer<T> c = EqualityComparer<T>.Default;
                for (int i = 0; i < _countX; i++)
                    for (int j = 0; j < _countY; j++)
                        if (c.Equals(_items[i, j], item)) return true;

                return false;
            }
        }

        public void CopyTo(Array array, int arrayIndex)
        {
            Array.Copy(_items, 0, array, arrayIndex, _countTotal);
        }

        public IEnumerator<Intersection2d<T>> GetEnumerator()
        {
            return new Enumerator(this);
        }

        public Index2d IndexOf(T intem)
        {
            throw new NotImplementedException();
        }

        public int IndexXOf(T item)
        {
            throw new NotImplementedException();
        }

        public int IndexYOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(IIndexXd index, T item)
        {
            throw new NotImplementedException();
        }

        public void InsertX(int x, T item)
        {
            throw new NotImplementedException();
        }

        public void InsertY(int y, T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(IIndexXd index, int dimension)
        {
            throw new NotImplementedException();
        }

        public void RemoveAtX(int x)
        {
            throw new NotImplementedException();
        }

        public void RemoveAtY(int y)
        {
            throw new NotImplementedException();
        }

        public bool RemoveX(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }

        public bool RemoveY(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }

        void ICollectionXd<T>.Add(IEnumerable<T> items, int dimension)
        {
            switch (dimension)
            {
                case 0:
                    AddX(items);
                    return;

                case 1:
                    AddY(items);
                    return;

                default:
                    throw new InvalidOperationException();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IIndexXd IMatrixXd<T>.IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        void IMatrixXd<T>.Insert(IIndexXd index, T item)
        {
            throw new NotImplementedException();
        }

        bool ICollectionXd<T>.Remove(IEnumerable<T> items, int dimension)
        {
            throw new NotImplementedException();
        }

        void IMatrixXd<T>.RemoveAt(IIndexXd index, int dimension)
        {
            throw new NotImplementedException();
        }

        // value for input, size for _sizeX or _sizeY
        private SizeAction CheckSizes(int value, int size)
        {
            if (value < size)
            {
                switch (AddingFewerItemsBehavior)
                {
                    case DimensionChangeBehavior.Throw:
                        return SizeAction.Throw;

                    case DimensionChangeBehavior.Ignore:
                        return SizeAction.Return;

                    case DimensionChangeBehavior.FillDefaults:
                        return SizeAction.ExtendInput;

                    default:
                        throw new NotImplementedException();
                }
            }

            if (value > size)
            {
                switch (AddingFewerItemsBehavior)
                {
                    case DimensionChangeBehavior.Throw:
                        return SizeAction.Throw;

                    case DimensionChangeBehavior.Ignore:
                        return SizeAction.Return;

                    case DimensionChangeBehavior.FillDefaults:
                        return SizeAction.ExtendItems;

                    default:
                        throw new NotImplementedException();
                }
            }

            return SizeAction.Nothing;
        }

        private SizeAction CheckSizeX(int value)
        {
            return CheckSizes(value, _countX);
        }

        private SizeAction CheckSizeY(int value)
        {
            return CheckSizes(value, _countY);
        }

        private void CopyArrayToItems(T[] input)
        {
            // hack to copy multidim array
            for (int i = 0, k = 0; i < _countX; i++)
            {
                for (int j = 0; j < _countY; j++, k++)
                {
                    _items[i, j] = input[k];
                }
            }
        }

        private void EnsureCapacities(Index2d min)
        {
            if (_capacityX < min.X || _capacityY < min.Y)
            {
                int newCapacityX = _capacityX == 0 ? _defaultCapacity : _capacityX * 2;
                int newCapacityY = _capacityY == 0 ? _defaultCapacity : _capacityY * 2;

                if ((uint)newCapacityX * (uint)newCapacityY > MaxArrayLength)
                {
                    // todo: don't know how to ensure
                    throw new NotImplementedException();
                }

                if (newCapacityX < min.X)
                    newCapacityX = min.X;
                if (newCapacityY < min.Y)
                    newCapacityY = min.Y;

                Capacities = new Index2d(newCapacityX, newCapacityY);
            }
        }

        private void EnsureCapacityX(int min)
        {
            if (_capacityX < min)
            {
                int newCapacity = _capacityX == 0 ? _defaultCapacity : _capacityX * 2;

                if ((uint)newCapacity * (uint)_capacityX > MaxArrayLength)
                    newCapacity = MaxArrayLength;

                if (newCapacity < min)
                    newCapacity = min;

                CapacityX = newCapacity;
            }
        }

        private void EnsureCapacityY(int min)
        {
            if (_capacityY < min)
            {
                int newCapacity = _capacityY == 0 ? _defaultCapacity : _capacityY * 2;

                if ((uint)newCapacity * (uint)_capacityY > MaxArrayLength)
                    newCapacity = MaxArrayLength;

                if (newCapacity < min)
                    newCapacity = min;

                CapacityY = newCapacity;
            }
        }

        private IList<T> EnsureListSizes(IEnumerable<T> items, int size, out int newSize)
        {
            var list = InputAsList(items);
            var action = CheckSizes(list.Count, size);
            switch (action)
            {
                case SizeAction.Nothing:
                    newSize = size;
                    return list;

                case SizeAction.Throw:
                    throw new ArgumentException();

                case SizeAction.ExtendItems:
                    newSize = list.Count;
                    return list;

                case SizeAction.ExtendInput:
                    newSize = size;
                    return list.Union(Enumerable.Repeat(default(T), size - list.Count)).ToArray();

                case SizeAction.Return:
                default:
                    newSize = size;
                    return null;
            }
        }

        private T[] InputAsArray(IEnumerable<T> items) => items is T[] a ? a : items.ToArray();

        private IList<T> InputAsList(IEnumerable<T> items) => items is IList<T> l ? l : items.ToArray();

        private enum SizeAction
        {
            Nothing,
            Return,
            Throw,
            ExtendItems,
            ExtendInput
        }

        /* todo: return interfaces and write custom linq */
        /*
        IEnumerator<IIntersectionXd<T>> IEnumerable<IIntersectionXd<T>>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Enumerator(this);
        }
        */

        [Serializable]
        public struct Enumerator : IEnumerator<Intersection2d<T>>, IEnumerator<T>, IEnumerator<IIntersectionXd<T>>, IEnumerator
        {
            private T _current;
            private Matrix2d<T> _matrix;
            private int _version;
            private int _x;
            private int _y;

            internal Enumerator(Matrix2d<T> matrix)
            {
                _matrix = matrix;
                _x = 0;
                // start from lower indexes to call ctor for Intersection only when Current called
                _y = -1;
                _version = matrix._version;
                _current = default;
            }

            public Intersection2d<T> Current => new Intersection2d<T>(_x, _y, _current);

            Object IEnumerator.Current
            {
                get
                {
                    //todo: check why it is like this in list class
                    //if (x == 0 || x == matrix._size + 1)
                    //{
                    //    ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
                    //}
                    return Current;
                }
            }

            T IEnumerator<T>.Current => _current;

            IIntersectionXd<T> IEnumerator<IIntersectionXd<T>>.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                Matrix2d<T> matrix = _matrix;

                if (_version == matrix._version && (uint)_x < (uint)matrix._countX)
                {
                    if ((uint)_y == (uint)matrix._countY - 1)
                    {
                        _y = -1;
                        _x++;
                        if ((uint)_x == (uint)matrix._countX)
                            return MoveNextRare();
                    }
                    _y++;
                    _current = matrix._items[_x, _y];
                    return true;
                }
                return MoveNextRare();
            }

            void IEnumerator.Reset()
            {
                if (_version != _matrix._version)
                {
                    throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
                }

                _x = 0;
                _y = -1;
                _current = default;
            }

            private bool MoveNextRare()
            {
                if (_version != _matrix._version)
                {
                    throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
                }

                // the same in list, but know why
                _x = _matrix._countX + 1;
                _y = _matrix._countY + 1;
                _current = default;
                return false;
            }
        }
    }
}