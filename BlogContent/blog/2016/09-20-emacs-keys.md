@{
    Layout = "post";
    Title = "Emacs-Keys";
    Date = "2016-09-20T08:42:41";
    Tags = "";
    Description = "Key keys for Emacs";
}

** EMACS: Key _Keys_ **
------------------

##### Files / Buffers #####

| Keys           | Command                                      | Keys           | Command                                                  |
|----------------|----------------------------------------------|----------------|----------------------------------------------------------|
| C-x C-f        | Open file                                    | C-x b <name>   | New buffer                                               |
| C-x C-s        | Save file                                    | C-x k          | Kill buffer                                              |
| C-x b          | Switch buffer                                | C-g            | Abandon command                                          |

##### Navigation #####

| Keys           | Command                                      | Keys           | Command                                                  |
|----------------|----------------------------------------------|----------------|----------------------------------------------------------|
| C-b            | Back 1 char                                  | C-f            | Forward 1 char                                           |
| M-b            | Back 1 word                                  | M-f            | Forward 1 word                                           |
| C-a            | Start of line                                | C-e            | End of line                                              |
| M-a            | Start of block                               | M-e            | End of block                                             |
| M-<            | Start of buffer                              | M->            | End of buffer                                            |
| M-v            | Back 1 screen                                | C-v            | Forwards 1 screen                                        |
| C-p            | Previous line (up)                           | C-n            | Next line (down)                                         |
| M-a            | Move first non-whitespace                    | Or...          | Just use the arrow keys                                  |

### _Even **more** Keys_ ###

##### Windows #####

| Keys           | Usage                                        | Keys           | Usage                                                    |
|----------------|----------------------------------------------|----------------|----------------------------------------------------------|
| C-x o q        | Close window                                 | C-x 1          | Delete all other windows                                 |
| C-x o          | Switch windows                               | C-x 2          | Split frame top / bottom                                 |
| C-x 0          | Delete current window                        | C-x 3          | Split frame left / right                                 |

##### Help / Search #####

| Keys           | Usage                                        | Keys           | Usage                                                    |
|----------------|----------------------------------------------|----------------|----------------------------------------------------------|
| C-h k <keys>   | Help on key binding                          | C-s            | Search (regex)                                           |
| C-h f <func>   | Describe function                            | C-r            | Search backwards                                         |
| M-g g          | Go to line                                   | C-c C-d C-a    | Free text doc search                                     |
|                |                                              | C-c C-d C-d    | Display documentation                                    |

#### _Editing **and** Packages_ ####

##### Editing #####

| Keys           | Usage                                        | Keys           | Usage                                                    |
|----------------|----------------------------------------------|----------------|----------------------------------------------------------|
| C-k            | Kill line                                    | M-w            | Copy to kill ring                                        |
| C-/            | Undo                                         | C-w            | Cut to kill ring                                         |
| C-space        | Start mark region                            | M-d            | Cut word                                                 |
| C-u n <cmd>    | Repeat command n times                       | C-y            | Yank from kill ring (paste)                              |
| M-/            | Expand                                       | M-y            | Kill selected text and replace it with last killed item  |
| M-\            | Delete surrounding space                     | C-j            | Newline and indent                                       |

##### Packages #####

| Keys                         | Usage                                        | Keys             | Usage                                                    |
|------------------------------|----------------------------------------------|------------------|----------------------------------------|
| M-x package-refresh-contents | Refresh list of known packages               | M-x clojure-mode | Enter clojure mode                     |
| M-x package-list-packages    | List packages                                |                  |                                        |
| M-x package-install <name>   | Install package                              |                  |                                        |

### _**Cider** REPL_ ###

| Keys              | Command                                      | Keys           | Command                                                    |
|-------------------|----------------------------------------------|----------------|------------------------------------------------------------|
| M-x cider-jack-in | Start CIDER clojure REPL                     | C-Enter        | Evaluate                                                   |
| C-x C-e           | Eval expression to left of cursor            | C-u C-x C-e    | Print to right of cursor, result of expr to left of cursor |
| C-x C-k           | Compile buffer into REPL                     | a              | In stack trace - expand / collapse details                 |
| M-, / M-.         | Navigate to source / back from source        | C-c M-n        | Set REPL namespace to match current file                   |
| C-upArrow         | Command history - back                       | C-downArrow    | Command history - forwards                                 |

#### _**Paredit** mode_ ####

| Keys              | Command                                      | Keys           | Command                                                    |
|-------------------|----------------------------------------------|----------------|------------------------------------------------------------|
| M-x paredit-mode  | Enable paredit                               | C-M-f          | Move forwards one expression                               |
| M-(               | Wrap brackets around point                   | C-M-b          | Move backwards one expression                              |
| C-rightArrow      | Slurp; move bracket after next token         |                |                                                            |
| C-leftArrow       | Barf; move bracket before next token         |                |                                                            |
