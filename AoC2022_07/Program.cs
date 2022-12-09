using AoC2022_7;

var input =
    """
    $ cd /
    $ ls
    dir a
    14848514 b.txt
    8504156 c.dat
    dir d
    $ cd a
    $ ls
    dir e
    29116 f
    2557 g
    62596 h.lst
    $ cd e
    $ ls
    584 i
    $ cd ..
    $ cd ..
    $ cd d
    $ ls
    4060174 j
    8033020 d.log
    5626152 d.ext
    7214296 k
    """.Split(Environment.NewLine);

var myInput = File.ReadAllLines("input.txt");

var root = new AoCDirectory("/",null);
var currentDir = root;
foreach(var line in myInput)
{
    if (line.StartsWith('$'))
    {
        var cmdParts = line.Split(' ') switch
        {
            [_, var cmd, var arg] => (cmd, arg),
            [_, var cmd] => (cmd, arg:""),
        };
        if(cmdParts.cmd == "cd")
        {
            if (cmdParts.arg == "/")
            {
                currentDir = root;
                continue;
            }
            if(cmdParts.arg == "..")
            {
                currentDir = currentDir.Parent;
                continue;
            }
            currentDir = currentDir.GetOrCreateChild(cmdParts.arg);
            continue;
        }
    }
    else
    {
        var fileParts = line.Split(' ') switch
        {
            [var part1, var part2] => (part1, part2),
        };

        if(fileParts.part1 == "dir")
        {
            currentDir.GetOrCreateChild(fileParts.part2);
            continue;
        }
        else
        {
            currentDir.GetOrCreateFile(fileParts.part2, fileParts.part1);
        }
    }
}
int answer1 = 0;

void TraverseDirectories(AoCDirectory dir)
{
    var size = dir.GetSize();
    if (size < 100_000)
        answer1 += size;
    foreach (var subdir in dir.Children.Values)
    {
        TraverseDirectories(subdir);
    }
}

TraverseDirectories(root);
Console.WriteLine(answer1);

int answer2 = int.MaxValue;
int totalSize = 70_000_000;
int need = 30_000_000;
int currentFreeSpace = totalSize - root.GetSize();
int toBeFreed = need - currentFreeSpace;

void TraverseDirectories2(AoCDirectory dir)
{
    var size = dir.GetSize();
    if (size > toBeFreed && size < answer2)
    {
        answer2 = size;
    }
    foreach (var subdir in dir.Children.Values)
    {
        TraverseDirectories2(subdir);
    }
}

TraverseDirectories2(root);

Console.WriteLine(answer2);


