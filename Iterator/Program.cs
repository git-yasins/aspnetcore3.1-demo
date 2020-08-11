using System;
using System.Collections.Generic;
/// <summary>
/// Iterator 迭代器模式 Foreach
/// </summary>
namespace Iterator {
    class Program {
        static void Main (string[] args) {
            ConcreteAggregate a = new ConcreteAggregate ();
            a[0] = "孙悟空";
            a[1] = "猪八戒";
            a[2] = "沙僧";
            a[3] = "白骨精";
            a[4] = "玉帝";

            Iterator iterator = new ConcreteIterator (a);
            object item = iterator.First ();
            while (!iterator.IsDone ()) {
                System.Console.WriteLine ($"{iterator.CurrentItem()}");
                iterator.Next ();
            }
        }
    }
    /// <summary>
    /// 迭代器抽像类
    /// </summary>
    abstract class Iterator {
        public abstract object First ();
        public abstract object Next ();
        public abstract bool IsDone ();
        public abstract object CurrentItem ();
    }

    /// <summary>
    /// 聚集抽像类
    /// </summary>
    abstract class Aggregate {
        /// <summary>
        /// 创建迭代器
        /// </summary>
        /// <returns></returns>
        public abstract Iterator CreateIterator ();
    }
    /// <summary>
    /// 具体聚集类
    /// </summary>
    class ConcreteAggregate : Aggregate {
        IList<object> items = new List<object> ();

        public override Iterator CreateIterator () {
            return new ConcreteIterator (this);
        }
        /// <summary>
        /// 返回聚集个
        /// </summary>
        /// <value></value>
        public int Count { get { return items.Count; } }
        /// <summary>
        /// 索引器
        /// </summary>
        /// <value></value>
        public object this [int index] {
            get { return items[index]; }
            set { items.Insert(index, value); }
        }
    }
    /// <summary>
    /// 具体迭代器
    /// </summary>
    class ConcreteIterator : Iterator {
        private ConcreteAggregate aggregate;
        private int current = 0;
        public ConcreteIterator (ConcreteAggregate aggregate) {
            this.aggregate = aggregate;
        }
        public override object CurrentItem () {
            return aggregate[current];
        }

        public override object First () {
            return aggregate[0];
        }

        public override bool IsDone () {
            return current >= aggregate.Count?true : false;
        }

        public override object Next () {
            object ret = null;
            current++;
            if (current < aggregate.Count) {
                ret = aggregate[current];
            }
            return ret;
        }
    }
}