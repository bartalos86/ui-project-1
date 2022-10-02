using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazniva_krizovatka.Tree
{
   /* public class BinaryNode<T> : BaseNode<T, BinaryNode<T>> where T : IComparable<T> {

    private int height = 1;
    private BinaryNode<T> parent = null;

    public BinaryNode(T data) : base(data)
    {
       
    }

    public int getHeight()
    {
        return height;
    }

    private int healtHelper(BinaryNode<T> node)
    {
        return node != null ? node.getHeight() : 0;
    }

    public void decrementHeight()
    {
        this.height--;

        if (this.hasParent())
            this.parent.setHeight(Math.Max(healtHelper(parent.getLeftNode()), healtHelper(parent.getRighNode())));
    }

    public void setHeight(int height)
    {

        //on insert
        if (this.hasParent())
            this.parent.setHeight(Math.Max(healtHelper(parent.getLeftNode()), healtHelper(parent.getRighNode())) + 1);

        this.height = height;


    }

    public BinaryNode<T> getParent()
    {
        return parent;
    }

    public void setParent(BinaryNode<T> parent)
    {
        this.parent = parent;
    }

    public int getBalance()
    {
        int leftValue = getLeftNode() != null ? getLeftNode().getHeight() : 0;
        int rightValue = getRighNode() != null ? getRighNode().getHeight() : 0;

        return leftValue - rightValue;
    }

    public BinaryNode<T> retrieveUnbalaced()
    {
        int balance = getBalance();

        if (balance > 1 || balance < -1)
        {
            return this;
        }
        else
        {
            if (this.hasParent())
            {
                return this.parent.retrieveUnbalaced();
            }
        }

        return null;

    }

    public void setLeftNodeNoParent(BinaryNode<T> leftNode)
    {
        base.setLeftNode(leftNode);
    }

    public void setRighNodeNoParent(BinaryNode<T> righNode)
    {
        base.setRighNode(righNode);
    }

   
    public override void setLeftNode(BinaryNode<T> leftNode)
    {
        if (leftNode != null)
            leftNode.setParent(this);
        base.setLeftNode(leftNode);

    }

    
    public override void setRighNode(BinaryNode<T> righNode)
    {
        if (righNode != null)
            righNode.setParent(this);
        base.setRighNode(righNode);

    }

    public bool hasParent()
    {
        return parent != null;
    }
}*/
}
