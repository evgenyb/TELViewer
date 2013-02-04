== How to create log4net database ==

Just create new database in you SQL Server and apply createdatabase.sql script.
After that change connection string setting

<property name="connection.connection_string">Data Source=xxx;Initial Catalog=xxx;Integrated Security=SSPI</property>

in nhibernate.config file. 

Note that you may also need to change it in the following files:
TELViewer\Config\Dev\nhibernate.config
TELViewer\Config\Production\nhibernate.config
TELViewer\Config\Staging\nhibernate.config



