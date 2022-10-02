using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazniva_krizovatka.Tree
{
   /* public class BinaryTree<T> : BaseTree<T, BinaryNode<T>> where T : IComparable<T> {

        public static BinaryNode<E> createNode(E data) 
            where E: IComparable<E>{
            return new BinaryNode<E>(data);
        }

        private int getHeight(BinaryNode<T> node)
        {
            return node != null ? node.getHeight() : 0;
        }

        public int insert(T data)
        {
            return this.insert(BinaryTree<T>.createNode(data));
        }

        private BinaryNode<T> rotateRight(BinaryNode<T> root)
        {
            BinaryNode<T> left = root.getLeftNode(); //this will be the new root
            BinaryNode<T> leftsRight = left.getRighNode();

            left.setParent(root.getParent());
            root.setParent(left);
            if (leftsRight != null)
                leftsRight.setParent(root);

            left.setRighNodeNoParent(root);
            root.setLeftNodeNoParent(leftsRight);

            root.setHeight(Math.Max(getHeight(root.getLeftNode()), getHeight(root.getRighNode())) + 1);
            left.setHeight(Math.Max(getHeight(left.getLeftNode()), getHeight(left.getRighNode())) + 1);

            return left;
        }

        private BinaryNode<T> rotateLeft(BinaryNode<T> root)
        {
            BinaryNode<T> right = root.getRighNode(); //this will be the new root
            BinaryNode<T> rightsLeft = right.getLeftNode();

            right.setParent(root.getParent());
            if (rightsLeft != null)
                rightsLeft.setParent(root);
            root.setParent(right);

            right.setLeftNodeNoParent(root);
            root.setRighNodeNoParent(rightsLeft);


            root.setHeight(Math.Max(getHeight(root.getLeftNode()), getHeight(root.getRighNode())) + 1);
            right.setHeight(Math.Max(getHeight(right.getLeftNode()), getHeight(right.getRighNode())) + 1);


            return right;
        }

        
     public override int insert(BinaryNode<T> node)
        {
            //int height = super.insert(node);

            //Override for Binarytree implementation
            if (root == null)
            {
                root = node;
                return 1;
            }

            BinaryNode<T> actualNode = root;
            int insertedPosition = 2;

            while (true)
            {

                int comparison = -actualNode.compare(node);
                if (comparison > 0)
                {

                    if (actualNode.getRighNode() == null)
                    {
                        actualNode.setRighNode(node);
                        // node.setParent(actualNode);
                        break;
                    }
                    actualNode = actualNode.getRighNode();

                }
                else if (comparison < 0)
                {

                    if (actualNode.getLeftNode() == null)
                    {
                        actualNode.setLeftNode(node);
                        //node.setParent(actualNode);
                        break;
                    }

                    actualNode = actualNode.getLeftNode();
                }

                insertedPosition++;
            }
            //End

            if (node.hasParent())
            {
                int leftValue = getHeight(node.getParent().getLeftNode());
                int rightValue = getHeight(node.getParent().getRighNode());

                node.getParent().setHeight(Math.max(leftValue, rightValue) + 1);

                //Detect and fix unbalance in the tree
                BinaryNode<T> unbalanced = node.retrieveUnbalaced();
                if (unbalanced != null)
                {
                    rebalance(unbalanced);
                }
            }


            return insertedPosition;
        }

     public override bool delete(T data)
        {

            //Override from BinaryTree
            BinaryNode<T> currentNode = root;
            BinaryNode<T> prevNode = null;
            int prevPos = 0;

            bool success = false;
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

                    //The root node is being deleted
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
                            success = true;
                            break;
                        }
                        else if (prevPos > 0)
                        {
                            prevNode.setRighNode(null);
                            success = true;

                            break;
                        }
                    }


                    //Scenario #2
                    if (currentNode.getRighNode() != null)
                    {

                        //Rights most left node
                        BinaryNode<T> mostLeftNode = currentNode.getRighNode();
                        while (mostLeftNode.getLeftNode() != null)
                        {
                            mostLeftNode = mostLeftNode.getLeftNode();
                        }

                        mostLeftNode.setLeftNode(currentNode.getLeftNode());

                        if (currentNode == root)
                        {
                            root = currentNode.getRighNode();
                            root.setParent(null);
                            success = true;
                            break;
                        }

                        if (prevPos < 0)
                        {
                            prevNode.setLeftNode(currentNode.getRighNode());
                            success = true;
                            break;
                        }
                        else if (prevPos > 0)
                        {
                            prevNode.setRighNode(currentNode.getRighNode());
                            success = true;
                            break;
                        }
                    }

                    if (currentNode.getLeftNode() != null)
                    {
                        if (currentNode == root)
                        {
                            root = currentNode.getLeftNode();
                            root.setParent(null);
                            success = true;
                            break;
                        }

                        if (prevPos < 0)
                        {
                            prevNode.setLeftNode(currentNode.getLeftNode());
                            success = true;
                            break;
                        }
                        else if (prevPos > 0)
                        {
                            prevNode.setRighNode(currentNode.getLeftNode());
                            success = true;
                            break;
                        }
                    }


                }
                else if (position < 0)
                {
                    prevNode = currentNode;
                    currentNode = currentNode.getLeftNode();
                }
                else if (position > 0)
                {
                    prevNode = currentNode;
                    currentNode = currentNode.getRighNode();
                }

                prevPos = position;

            }
            if (success && prevNode != null)
            {
                prevNode.setHeight(prevNode.getHeight());//Update height
                rebalance(prevNode);
            }

            return success;
        }

        private BinaryNode<T> rebalance(BinaryNode<T> node)
        {

            if (node == null) return null;
            int balance = node.getBalance();
            BinaryNode<T> parent = node.getParent();

            int position = 0;
            if (node.hasParent())
            {
                if (node.getParent().getLeftNode() == node) position = -1;
                if (node.getParent().getRighNode() == node) position = 1;
            }

            BinaryNode<T> rebalanced = null;

            if (balance > 1)
            {
                rebalanced = rotateRight(node);
            }
            else if (balance < -1)
            {
                rebalanced = rotateLeft(node);
            }

            if (rebalanced != null)
            {
                if (parent != null)
                {

                    if (position == -1) parent.setLeftNode(rebalanced);
                    if (position == 1) parent.setRighNode(rebalanced);
                }
                else
                {
                    root = rebalanced;
                    root.setParent(null);
                }
            }


            return rebalance(parent);

        }

     public override void traverseInOrderRecursive(BinaryNode<T> currentNode, int lvl)
        {

            if (currentNode != null)
            {
                traverseInOrderRecursive(currentNode.getLeftNode(), lvl + 1);
                Console.WriteLine(currentNode.GetData() + " balance: " + currentNode.getBalance() + " height: " + currentNode.getHeight() + " lvl: " + lvl);
                traverseInOrderRecursive(currentNode.getRighNode(), lvl + 1);
            }

        }

    }*/
}
