Server Management With SNMP

You must create a user for SNMP Network Protocol

> mkdir /etc/snmp
> cd /etc/snmp
> echo -e  "rocommunity public\nrwcommunity public" > /etc/snmp/snmpd.conf

How to use ?

> http://localhost:5000/api/snmp?identifier=OID

Example OID -> 1.3.6.1.2.1.25.3.3.1.2

![alt text](https://media-s3.paessler.com/kb/2017/653-OID+tree.png)
