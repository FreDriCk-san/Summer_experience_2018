TASK 1 "Client-server application for text chat"



It is proposed to create a simple (local) client-server application for organizing a simple text
chat. The server must be able to accept incoming connections from several clients (maximum 10),
authorization is not required. Messages from each client are sent to all other customers.
Incoming and outgoing messages are output to the console.



Additional tasks for Task 1:


1. Add the server the ability to run in daemon mode.

2. In the case of the first supplement. tasks, add server command line parameters to terminate an already running application instance.

3. Add to the server a check for the ability to run only one copy at a time.

4. Add customers the opportunity to see the number of other online customers.

5. Add an exit command from the chat (client closure) with notification to other clients.



Completed: 4, 5.



How to start:

1) If you need a server, call a script named "ServerStart.sh" [Example 1].



//----------Example 1----------//

cd ...Your path to folder "Summer Tasks".../Task_1/

sh ServerStart.sh

//-----------------------------//




2) If you need a client, call a script named "ClientStart.sh" [Example 2].



//----------Example 2----------//

cd ...Your path to folder "Summer Tasks".../Task_1/

sh ClientStart.sh

//-----------------------------//




If you are a Windows user, just execute the ".bat" files:
1) "ClientStart.bat" for client side.
2) "ServerStart.bat" for server side.