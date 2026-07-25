using System;

namespace Core.Object.Module
{
    /// <summary>
    /// Represents a node in a tree structure with directional child references.
    /// </summary>
    public interface INode
    {
        /// <summary>
        /// Gets the child node attached at <paramref name="direction"/>.
        /// </summary>
        /// <param name="direction">The direction to query.</param>
        /// <returns>The child node attached at that direction, or <c>null</c> if none exists.</returns>
        public INode GetChildNode(Direction direction);

        /// <summary>
        /// Determines whether a child node is attached at <paramref name="direction"/>.
        /// </summary>
        /// <param name="direction">The direction to query.</param>
        /// <returns><c>true</c> if a child is attached at that direction; otherwise, <c>false</c>.</returns>
        public bool HasChildNode(Direction direction);

        /// <summary>
        /// Attempts to get the child node attached at <paramref name="direction"/>.
        /// </summary>
        /// <param name="direction">The direction to query.</param>
        /// <param name="child">The child node attached at that direction, or <c>null</c> if none exists.</param>
        /// <returns><c>true</c> if a child was found; otherwise, <c>false</c>.</returns>
        public bool TryGetChildNode(Direction direction, out INode child);

        /// <summary>
        /// Gets the parent node this node is attached to, or <c>null</c> if this node is the root.
        /// </summary>
        public INode ParentNode { get; }

        /// <summary>
        /// Gets whether this node is the root of the tree.
        /// </summary>
        public bool IsRootNode { get; }
    }
}
