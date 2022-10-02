using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace blazniva_krizovatka.Tree
{
  /*  public class BaseNode<T, NODE> : ICustomComparable<T>
        where T : IComparable<T>
        where NODE : BaseNode<T, NODE>
    {

        private T data;
        private NODE leftNode;
        private NODE righNode;

        public BaseNode(T data)
        {
            this.data = data;
        }

        public bool hasChildren()
        {
            return leftNode != null || righNode != null;
        }


        public NODE getLeftNode()
        {
            return leftNode;
        }

        public virtual void setLeftNode(NODE leftNode)
        {
            this.leftNode = leftNode;
        }

        public NODE getRighNode()
        {
            return righNode;
        }

        public virtual void setRighNode(NODE righNode)
        {
            this.righNode = righNode;
        }

        public void setData(T data)
        {
            this.data = data;
        }

        //Own interface implementation
        public int Compare(ICustomComparable<T> node)
        {
            return data.CompareTo(node.GetData());
        }

        public T GetData()
        {
            return data;
        }

    }*/
}
