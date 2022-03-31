# ModularMonolith
Modular monolith sample. 
Run CreateDatabase.sql to start application.

Master branch contains version based on Autofac DI container, db transactions, Assembly.Load for MediatR and AutoMapper.

DistributedTransactions branch contains version based on MS DI container, TransactionScope and registered in DI conntainer assembly markers for MediatR and AutoMapper.