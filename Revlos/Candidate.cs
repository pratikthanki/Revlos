using System.Collections;
using System.Text;

namespace Revlos
{
    public class Candidate : IEnumerable
    {
        private readonly bool[] _values;
        public readonly int _candidateNumber;
        public int Count { get; private set; }

        public Candidate(int candidateNumber, bool initialValue)
        {
            _values = new bool[candidateNumber];
            Count = 0;
            _candidateNumber = candidateNumber;

            for (var i = 1; i <= candidateNumber; i++)
                this[i] = initialValue;
        }

        public bool this[int key]
        {
            get => _values[key - 1];

            set
            {
                Count += _values[key - 1] == value ? 0 : value ? 1 : -1;
                _values[key - 1] = value;
            }
        }

        public void SetAll(bool value)
        {
            for (var i = 1; i <= _candidateNumber; i++)
                this[i] = value;
        }

        public IEnumerator GetEnumerator()
        {
            return new CandidateEnumerator(this);
        }

        public override string ToString()
        {
            var values = new StringBuilder();
            foreach (int candidate in this)
                values.Append(candidate);

            return values.ToString();
        }
    }

    public class CandidateEnumerator : IEnumerator
    {
        private int _matrixPosition;
        private readonly Candidate _matrixCandidate;
        public object Current => _matrixPosition;

        public CandidateEnumerator(Candidate candidate)
        {
            _matrixCandidate = candidate;
            _matrixPosition = 0;
        }

        public bool MoveNext()
        {
            ++_matrixPosition;
            if (_matrixPosition <= _matrixCandidate._candidateNumber)
                return _matrixCandidate[_matrixPosition] || MoveNext();

            return false;
        }

        public void Reset()
        {
            _matrixPosition = 0;
        }
    }
}
