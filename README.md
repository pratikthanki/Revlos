## Revlos

In Japanese, Su Doku (数独, “Unmarried Numbers”).

A “sudoku square” is a 9 × 9 array that has been divided into 3 × 3 boxes and filled with the 
digits {1, 2, 3, 4, 5, 6, 7, 8, 9} in such a way that;
- every row contains each of the digits {1, 2, 3, 4, 5, 6, 7, 8, 9} exactly once;
- every column contains each of the digits {1, 2, 3, 4, 5, 6, 7, 8, 9} exactly onc
- every box contains each of the digits {1, 2, 3, 4, 5, 6, 7, 8, 9} exactly once.

Since there are nine cells in each row, each column, and each box, the words ‘exactly once’ can be replaced 
by ‘at least once’ or ‘at most once’, anywhere in this definition.

Though there are more original problems to solve, I wanted to implement my own Sudoku Solver.

This is written in C# by considering it as an [exact cover](https://en.wikipedia.org/wiki/Exact_cover) 
problem I chose to implement this using Donald E. Knuth's Algorithm X and the Dancing Links technique as described 
in his [paper](https://arxiv.org/abs/cs/0011047).

[VOLUME 4 PRE-FASCICLE 5C](https://www.inf.ufrgs.br/~mrpritt/lib/exe/fetch.php?media=inf5504:7.2.2.1-dancing_links.pdf) of the 
_The Art of Computer Programming_ series looks at Dancing Links in more detail. In my opinion, `Volume 1: Fundamental Algorithms` is a 
book every devloper should read.

He presented a great talk at the Stanford Annual Christmas [Lecture](https://www.youtube.com/watch?v=_cR9zDlvP88) 
where he descibed the algorithm.

You can find more details about the books [here](https://www-cs-faculty.stanford.edu/~knuth/taocp.html).