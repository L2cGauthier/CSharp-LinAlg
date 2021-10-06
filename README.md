# CSharp-LinAlg

This project is a simple, compact and fairly standalone linear algebra library. It can easily be used in the context of a Unity project, but doesn't have to.

The library is centered around the Matrix2D class - that represents a 2-dimensional matrix - and several functions to interact with instances of the class. Most functionalities are contained inside the LinAlg namespace. The LinAlg.Utility contains helper functions, and the LinAlg.Tests contains tests and test runners.

The library includes the following key features:

* Matrix inversion (Gauss-Jordan elimination);
* LU Decomposition (Doolittle);
* Matrix determinant & invertibility;
* Matrix product;
* Scalar operations;
* Row-wise & column-wise operations.
