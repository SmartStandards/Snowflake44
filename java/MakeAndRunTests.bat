REM https://jdk.java.net/25/
REM Add a system variable named "JAVA_HOME" that points to the location of the JDK install, e.g.:
REM SETX /M JAVA_HOME C:\Progs\jdk-25.0.1
REM SETX /M Path "%Path%;C:\Progs\jdk-25.0.1\bin" 
REM Check via C:\Windows\system32\rundll32.exe sysdm.cpl,EditEnvironmentVariables

javac.exe EncodedTokenTests.java

java EncodedTokenTests
