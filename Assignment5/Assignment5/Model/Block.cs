using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5.Model
{
    public abstract class Block
    {
        /// <summary>
        /// Class for blocks
        /// </summary>
        public abstract int Id { get; }
        protected abstract Position[][] Form { get; }
        protected abstract Position StartPosition { get;  }
        private int _state;
        private readonly Position _position;

        public Block()
        {
            _position = new Position(StartPosition.Row, StartPosition.Column);
            _state = 0;            
        }
        /// <summary>
        /// Yields new position based on the blocks rotation state
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Position> FormPositions()
        {
            foreach(Position p in Form[_state])
            {
                yield return new Position(p.Row + _position.Row, p.Column + _position.Column);
            }
        }
        /// <summary>
        /// Method to rotate
        /// </summary>
        public void RotateClockWise()
        {         
            _state = (_state + 1) % Form.Length;
        }
        /// <summary>
        /// Method to rotate
        /// </summary>
        public void RotateCounterClockWise() 
        { 
            if(_state == 0)
            {
                _state = Form.Length - 1;
            } 
            else
            {
                _state--;
            }        
        }
        /// <summary>
        /// Method to move a block
        /// </summary>
        /// <param name="rows">Row-step to move</param>
        /// <param name="columns">Column-step to move</param>
        public void Move(int rows, int columns)
        {
            _position.Row += rows;
            _position.Column += columns;
        }
        /// <summary>
        /// Reset state of block when new block is generated
        /// </summary>
        public void Reset()
        {
            _state = 0;
            _position.Row = StartPosition.Row;
            _position.Column = StartPosition.Column;
        }
    }
}
