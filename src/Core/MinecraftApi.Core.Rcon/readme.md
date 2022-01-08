This project is based of https://github.com/willroberts/minecraft-client-csharp used under GPL v3.0 license.

Thank you!

Remember that this project is also shared under the same license!

This project is as basic as it gets.

The idea is that it handles only the lowest level communication with RCON all in bytes at first.

The project will also be able to handle string commands. But it will ALWAYS remain agnostic as to what commands it's sending.

The objective is to keep it as light and general as possible and only the less general Core will have the specializations to run only a sub set of commands.