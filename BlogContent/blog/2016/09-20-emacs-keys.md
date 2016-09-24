@{
    Layout = "post";
    Title = "emacs-keys";
    Date = "2016-09-20T08:42:41";
    Tags = "";
    Description = "";
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
| M-g g          | Go to line                                   | C-c C-d C-a    | Free text search of documentation                        |
|                |                                              | C-c C-d C-d    | Display documentation                                    |

#### _Editing **and** Modes_ ####

##### Editing #####

| Keys           | Usage                                        | Keys           | Usage                                                    |
|----------------|----------------------------------------------|----------------|----------------------------------------------------------|
| C-k            | Kill line                                    | M-w            | Copy to kill ring                                        |
| C-/            | Undo                                         | C-w            | Cut to kill ring                                         |
| C-space        | Start mark region                            | M-d            | Cut word                                                 |
|                |                                              | C-y            | Yank from kill ring (paste)                              |
| M-/            | Expand                                       | M-y            |                                                          |
| M-\            | Delete surrounding space                     | C-j            | Newline and indent                                       |

##### Modes #####

| Keys                         | Usage                                        | Keys           | Usage                                                    |
|------------------------------|----------------------------------------------|----------------|----------------------------------------------------------|
| M-x package-refresh-contents |                                              |                |                                                          |
| M-x package-list-packages    |                                              |                |                                                          |
| M-x package-install          |                                              |                |                                                          |

##### Further Info #####

