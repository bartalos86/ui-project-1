using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace blazniva_krizovatka.Tree
{
   /* public class BaseTree<T, NODE>
        where T : IComparable<T>?
        where NODE : BaseNode<T, NODE>
    {

        //Node
        protected NODE root;


        public virtual int insert(NODE node)
        {

            if (root == null)
            {
                root = node;
                return 1;
            }

            NODE actualNode = root;
            int insertedPosition = 2;

            while (true)
            {

                int comparison = -actualNode.Compare(node);
                if (comparison > 0)
                {

                    if (actualNode.getRighNode() == null)
                    {
                        actualNode.setRighNode(node);
                        return insertedPosition;
                    }
                    actualNode = actualNode.getRighNode();

                }
                else if (comparison < 0)
                {

                    if (actualNode.getLeftNode() == null)
                    {
                        actualNode.setLeftNode(node);
                        return insertedPosition;
                    }

                    actualNode = actualNode.getLeftNode();
                }

                insertedPosition++;
            }

        }

        public T search(T data)
        {
            NODE currentNode = root;
            while (true)
            {

                if (currentNode == null)
                {
                    return default;
                }
                int position = -currentNode.GetData().CompareTo(data);

                if (position == 0)
                {
                    return currentNode.GetData();
                }
                else if (position < 0)
                {
                    currentNode = currentNode.getLeftNode();
                }
                else if (position > 0)
                {
                    currentNode = currentNode.getRighNode();
                }
            }
        }

        public virtual bool delete(T data)
        {
            NODE currentNode = root;
            NODE prevNode = null;
            int prevPos = 0;
            int level = 0;
            while (true)
            {

                if (currentNode == null)
                {
                    return false;
                }

                int position = -currentNode.GetData().CompareTo(data);


                //The node has been found
                if (position == 0)
                {

                    //If its a leaf
                    if (!currentNode.hasChildren())
                    {

                        //In case of root set the tree to null
                        if (currentNode == root)
                        {
                            root = null;
                            return true;
                        }

                        if (prevPos < 0)
                        {
                            prevNode.setLeftNode(null);
                            return true;
                        }
                        else if (prevPos > 0)
                        {
                            prevNode.setRighNode(null);
                            return true;
                        }
                    }


                    //Scenario #2
                    if (currentNode.getRighNode() != null)
                    {


                        //Rights most left node
                        NODE mostLeftNode = currentNode.getRighNode();
                        while (mostLeftNode.getLeftNode() != null)
                        {
                            mostLeftNode = mostLeftNode.getLeftNode();
                        }

                        mostLeftNode.setLeftNode(currentNode.getLeftNode());


                        if (currentNode == root)
                        {
                            root = currentNode.getRighNode();
                            return true;
                        }

                        if (prevPos < 0)
                        {
                            prevNode.setLeftNode(currentNode.getRighNode());
                            return true;
                        }
                        else if (prevPos > 0)
                        {
                            prevNode.setRighNode(currentNode.getRighNode());
                            return true;
                        }
                    }

                    if (currentNode.getLeftNode() != null)
                    {
                        if (currentNode == root)
                        {
                            root = currentNode.getLeftNode();
                            return true;
                        }

                        if (prevPos < 0)
                        {
                            prevNode.setLeftNode(currentNode.getLeftNode());
                            return true;
                        }
                        else if (prevPos > 0)
                        {
                            prevNode.setRighNode(currentNode.getLeftNode());
                            return true;
                        }
                    }


                }
                else if (position < 0)
                {
                    prevNode = currentNode;
                    currentNode = currentNode.getLeftNode();
                    level++;
                }
                else if (position > 0)
                {
                    prevNode = currentNode;
                    currentNode = currentNode.getRighNode();
                    level++;
                }

                prevPos = position;
            }
        }


        public virtual void traverseInOrderRecursive(NODE currentNode, int lvl)
        {

            if (currentNode != null)
            {
                traverseInOrderRecursive(currentNode.getLeftNode(), lvl + 1);
                Console.WriteLine(currentNode.GetData() + " - Level: " + lvl);
                traverseInOrderRecursive(currentNode.getRighNode(), lvl + 1);
            }

        }

        public void printTree()
        {

            Console.WriteLine("#########Tree###########");

            traverseInOrderRecursive(root, 0);

            Console.WriteLine("###########end tree#############");
        }
    }*/
}
