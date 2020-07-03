using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Revlos
{
    public class BoardSquare
    {
        private readonly HashSet<int> _possibleValues = new HashSet<int>();
        private int? _value; 

        public BoardSquare(int? value)
        {
            _value = value;
        }
        public BoardSquare()
        {
            _value = null;
        }

        public bool IsSolved()
        {
            return _value != null;
        }

        public void SetValue(int? value)
        {
            if (_possibleValues.Count > 0)
            {
                _possibleValues.Clear();
                _value = value;
            }
            else
            {
                _value = value;
            }
        }

        public void AddPossibleValue(int value)
        {
            _possibleValues.Add(value);
        }

        public void RemovePossibleValue(int value)
        {
            if (_possibleValues.Count > 0)
            {
                _possibleValues.Remove(value);
            }
        }

        public HashSet<int> GetPossibleValues()
        {
            return _possibleValues;
        }

        public int? GetValue()
        {
            return _value;
        }

        public override string ToString()
        {
            return _value == null ? " " : _value.ToString();
        }
    }
}
