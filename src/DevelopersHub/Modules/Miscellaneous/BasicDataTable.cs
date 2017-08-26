using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web;

using System.Collections;


    public interface IGenericDataTable : IEnumerable
    {
        List<string> ColumnNames
        {
            get;
            set;
        }

        List<IGenericDataRow> Records
        {
            get;
            set;
        }

        IGenericDataRow this[int index]
        {
            get;
            set;
        }
    }
    public interface IGenericDataRow : IEnumerable
    {
        int ColumnCount
        {
            get;
        }
        object this[int index]
        {
            get;

        }
        object this[string columnname]
        {
            get;
            set;
        }
    }





    public abstract class GenericDataTable : IGenericDataTable
    {
        List<string> IGenericDataTable.ColumnNames
        {
            get
            {
                return this.ColumnNames;
            }
            set
            {
                this.ColumnNames = value;
            }
        }

        public virtual List<string> ColumnNames
        {
            get;
            set;
        }


        List<IGenericDataRow> IGenericDataTable.Records
        {
            get
            {
                return this.Records;
            }
            set
            {
                this.Records = value;
            }
        }

        IGenericDataRow IGenericDataTable.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = value;
            }
        }

        public virtual IGenericDataRow this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = value;
            }
        }


        public virtual List<IGenericDataRow> Records
        {
            get;
            set;
        }

        public IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }



        public struct Enumerator : IEnumerator, IDisposable
        {
            private IGenericDataTable _genericdatatable;
            private int _curindex;
            public Enumerator(IGenericDataTable genericdatatable)
            {
                _curindex = -1;
                _genericdatatable = genericdatatable;
            }
            public object Current { get { return _genericdatatable[_curindex]; } }
            public void Dispose()
            {
                _genericdatatable = null;
            }
            public bool MoveNext()
            {
                if (_genericdatatable.Records.Count - 1 > _curindex)
                {
                    _curindex++;
                    return true;
                }
                else
                {
                    return false;
                }

            }
            public void Reset()
            {
                _curindex = -1;
            }
        }

    }


    public abstract class GenericDataRow : IGenericDataRow
    {
        int IGenericDataRow.ColumnCount
        {
            get
            {
                return this.ColumnCount;
            }
        }
        object IGenericDataRow.this[int index]
        {
            get
            {
                return this[index];
            }

        }
        object IGenericDataRow.this[string columnname]
        {
            get
            {
                return this[columnname];
            }
            set
            {
                this[columnname] = value;
            }
        }
        public abstract object this[string columnname]
        {
            get;
            set;
        }
        public abstract object this[int index]
        {
            get;

        }

        public IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public abstract int ColumnCount
        {
            get;
        }


        public struct Enumerator : IEnumerator, IDisposable
        {
            private IGenericDataRow _genericdatarow;
            private int _curindex;
            public Enumerator(IGenericDataRow genericdatarow)
            {
                _curindex = -1;
                _genericdatarow = genericdatarow;
            }
            public object Current { get { return _genericdatarow[_curindex]; } }
            public void Dispose()
            {
                _genericdatarow = null;
            }
            public bool MoveNext()
            {
                if (_genericdatarow.ColumnCount - 1 > _curindex)
                {
                    _curindex++;
                    return true;
                }
                else
                {
                    return false;
                }

            }
            public void Reset()
            {
                _curindex = -1;
            }
        }

    }



    public class BasicDataTable : GenericDataTable
    {
        public BasicDataTable()
        {
        }
        public BasicDataTable(List<string> columnnames)
        {
            ColumnNames = columnnames;
        }
        public BasicDataTable(List<IGenericDataRow> records)
        {
            base.Records = records;
        }
        public override List<string> ColumnNames
        {
            get
            {
                return base.ColumnNames;
            }
            set
            {
                base.ColumnNames = value;
            }
        }
        public override IGenericDataRow this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                base[index] = value;
            }
        }
    }
    public class BasicDataRow : GenericDataRow
    {
        private GenericDataTable _genericdatatable;
        public BasicDataRow(GenericDataTable genericdatatable)
        {
            _genericdatatable = genericdatatable;
        }
        public BasicDataRow(List<object> record, GenericDataTable genericdatatable)
            : this(genericdatatable)
        {
            _record = record;
        }


        private List<object> _record;
        public override object this[string columnname]
        {
            get
            {
                int __index = -1;
                for (int i = 0; i < _genericdatatable.ColumnNames.Count; i++)
                {
                    if (columnname == _genericdatatable.ColumnNames[i])
                    {
                        __index = i;
                        break;
                    }
                }
                if (__index > -1)
                    return _record[__index];
                else
                    return null;
            }
            set
            {
                int __index = -1;
                for (int i = 0; i < _genericdatatable.ColumnNames.Count; i++)
                {
                    if (columnname == _genericdatatable.ColumnNames[i])
                    {
                        __index = i;
                        break;
                    }
                }
                if (__index > -1)
                    _record[__index] = value;
            }
        }

        public override object this[int index]
        {
            get
            {

                return _record[index];
            }

        }

        public override int ColumnCount
        {
            get
            {
                return _genericdatatable.ColumnNames.Count;
            }
        }
    }