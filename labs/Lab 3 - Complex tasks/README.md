# Lab 3 - Complex tasks: Advanced Features of GitHub Copilot

This lab exercise delves into GitHub Copilot's advanced features, teaching participants to enhance coding efficiency through complex tasks like adding new properties, generating documentation, refactoring code, and parsing strings, supplemented by optional labs on context understanding and regex parsing.

> [!IMPORTANT]
> While GitHub Copilot is a powerful tool, it’s not infallible. The responses it generates can sometimes be incorrect or not exactly what you intended. This is part of the challenge and learning experience. During the workshop, we encourage you to experiment with modifying your prompts to guide GitHub Copilot towards generating the correct code.

## Time Required

- 40 minutes

## Goals

- To master GitHub Copilot's advanced features for solving complex coding exercises and optimizing code.

### Step 1: The complete collection

- Open `BikeShopAPI` folder located in the `src` folder.

- Open the `Entities/BikeShop.cs` file.

- Add a `ImageUrl` property to the model.

- Type `public string? ImageUrl { get; set; }` in the `BikeShop.cs` file.

```csharp
public class BikeShop
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public bool HasDelivery { get; set; }
    public int AddressId { get; set; }
    public virtual List<Bike>? Bikes { get; set; }
    public ShopStatus Status { get; set; }
    // New property
    public string? ImageUrl { get; set; }
}
```

- Open the `Controllers/BikeShopController.cs` file.

- Select all content of the `BikeShops` List. You can find it at line `18`.

- Right click and select the option `Copilot` -> `Editor Inline Chat`. You can also use `CTRL+I` shortcut instead.

- Type the following command

    ```text
    Add the new ImageUrl property to each bike shop and add the next 2 additional bike shops to complete the Wheel Brothers Collection.
    ```

    ![Bikes List](../../img//lab-3-1-bikes-list.png)

- Accept the suggestion by selecting `Accept` or pressing `Enter`.

    GitHub Copilot can do more than one thing at a time. It added the new property to each bike shop and next Wheel Brothers bike shop to the list of bike shops.

### Step 2: Documenting your code

- Open the `BikeShopAPI` folder located in the `src` folder.

- Open the `Controllers/BikeShopController.cs` file.

- Select all content of the method `GetById` in the `BikeShopController.cs` file.

- Right click and choose for the option `Copilot` -> `Generate Docs`.

    ![Controller Docs](../../img/lab-3-2-controller-docs.png)

    ![Controller Docs](../../img/lab-3-2-controller-docs-2.png)

- Do not accept the suggestion, click `Close`.

    GitHub Copilot used the `/docs` agent to generate the documentation for a single method or the entire file in a matter of seconds. This is a great way to document your codebase quickly and efficiently. However, we will use the Inline Chat to document the code in a more controlled way.

- Let's try this using a different approach, select all content of the method `GetById` in the `BikeShopController.cs` file.

- Open GitHub Copilot Chat, click **+** to clear prompt history, then type the following command:

Compare the difference between asking the two following things:

1. Simple:

    ```text
    Document all selected code
    ```

2. More details using dotnet method documentation

    ```text
    document selected code with details using dotnet method documentation
    ```

- Review the documentation to ensure it's accurate, then click on `Insert at cursor` to replace the `BikeShopController.cs` methods with the new documentation.

    ![Controller Docs](../../img/lab-3-2-controller-docs-3.png)

    The difference is that Inline Chat does light documentation vs GitHub Copilot window does a thorough job documenting every important section of code.

### Step 3: Code Refactoring

- Open the `Controllers/BikeShopController.cs` file.

- Navigate to the `UpdateShopStatus` method.

    ```csharp
    public class BikeShopController : ControllerBase
    {

        // Other methods
        [HttpPost("{id}/status")]
        public ActionResult UpdateShopStatus(int id, ShopStatus newStatus)
        {
            var bikeShop = BikeShops.Find(b => b.Id == id);
            if (bikeShop != null)
            {
                switch (newStatus)
                {
                    case ShopStatus.Open:
                        if (bikeShop.Status == ShopStatus.Renovating)
                        {
                            return BadRequest("Cannot reopen, shop is currently renovating.");
                        }
                        break;

                    case ShopStatus.Closed:
                        if (bikeShop.Status == ShopStatus.Renovating)
                        {
                            return BadRequest("Cannot close, shop is currently renovating.");
                        }
                        break;

                    case ShopStatus.Renovating:
                        if (bikeShop.Status == ShopStatus.Open)
                        {
                            return BadRequest("Shop must be closed before starting renovations.");
                        }
                        break;

                    default:
                        return BadRequest("Unknown or unsupported shop status.");
                }

                bikeShop.Status = newStatus;

                return Ok($"Bike shop status updated to {newStatus}.");
            }
            else
            {
                return NotFound("Bike shop not found.");
            }
        }
    }
    ```

    Note that the `UpdateShopStatus` method has a high code complexity rating. This is a good candidate for refactoring.

- Select all the content of the `UpdateShopStatus` method.

- Open GitHub Copilot Chat, click **+** to clear prompt history.

- Ask the following question:

    ```text
    Refactor the selected code to make it more readable and maintainable.
    ```

    ![update-shop-status-refactor](../../img/lab-3-3-update-shop-status-refactor.png)

    GitHub Copilot Chat understands `the selected code`. It will use the selected code in your editor to generate the refactoring suggestions.

- Copilot Chat suggests a code improvement to extract some of the complex code to their own methods to make the code more readible and maintainable:

    ```csharp
    [HttpPost("{id}/status")]
    public ActionResult UpdateShopStatus(int id, ShopStatus newStatus)
    {
        var bikeShop = BikeShops.Find(b => b.Id == id);
        if (bikeShop == null)
        {
            return NotFound("Bike shop not found.");
        }

        var errorMessage = ValidateStatusChange(bikeShop.Status, newStatus);
        if (errorMessage != null)
        {
            return BadRequest(errorMessage);
        }

        bikeShop.Status = newStatus;

        return Ok($"Bike shop status updated to {newStatus}.");
    }

    private static string? ValidateStatusChange(ShopStatus currentStatus, ShopStatus newStatus)
    {
        var statusChangeErrors = new Dictionary<(ShopStatus current, ShopStatus newStatus), string>
        {
            {(ShopStatus.Renovating, ShopStatus.Open), "Cannot reopen, shop is currently renovating."},
            {(ShopStatus.Renovating, ShopStatus.Closed), "Cannot close, shop is currently renovating."},
            {(ShopStatus.Open, ShopStatus.Renovating), "Shop must be closed before starting renovations."}
        };

        if (statusChangeErrors.TryGetValue((currentStatus, newStatus), out var errorMessage))
        {
            return errorMessage;
        }

        if (!Enum.IsDefined(typeof(ShopStatus), newStatus))
        {
            return "Unknown or unsupported shop status.";
        }

        return null;
    }
    ```

    The output of Copilot chat can vary, but the output should be a refactored method that is more readable and maintainable.

    Note that GitHub Copilot Chat can make mistakes sometimes. Best practice is to have the method covered with unit tests before refactoring it. This is not a requirement for this lab, but it is a good practice to follow. These unit tests can be generated by GitHub Copilot as well, which is covered in a previous lab.

### Step 4: Prompt Engineering

Before we explore specific strategies, let's first understand the basic principles of prompt engineering, summed up in the 4 Ss below. These core rules are the basis for creating effective prompts.

- **Single**: Always focus your prompt on a single, well-defined task or question. This clarity is crucial for eliciting accurate and useful responses from Copilot.
- **Specific**: Ensure that your instructions are explicit and detailed. Specificity leads to more applicable and precise code suggestions.
- **Short**: While being specific, keep prompts concise and to the point. This balance ensures clarity without overloading Copilot or complicating the interaction.
- **Surround**: Utilize descriptive filenames and keep related files open. This provides Copilot with rich context, leading to more tailored code suggestions.

These core principles lay the foundation for crafting efficient and effective prompts. Keeping the 4 Ss in mind, let's dive deeper into advanced best practices that ensure each interaction with GitHub Copilot is optimized.

- Open the `Entities/Bike.cs` file.

- Add new `RideLogSignatures` property that is a list of `string` type.

    ```csharp
    public class Bike
    {
        // Other properties
        public List<string> RideLogSignatures { get; set; } = new List<string>();
    }
    ```

    Note that the `RideLogSignatures` is a fictional property that is used to demonstrate the capabilities of GitHub Copilot. It is not a real cycling concept.

- Open GitHub Copilot Chat in the **Ask** mode, click **+** to clear prompt history, then ask the following question:

    ```text
    Create a C# model for a RideLogSignatures property.

    Example: 17121903-START-END-ROUTE

    17th of December 1903
    Start from Kitty Hawk, NC
    End at Manteo, NC
    Route name: WB001
    
    ## Technical Requirements
    Create a RideLog record type
    Add a Parse method to the RideLog record type
    The Date must be stored as a DateTime type
    ```

- The prompt contains a few-shot prompting example of a `RideLogSignatures` and a few technical requirements.

    Few-Shot prompting is a concept of prompt engineering. In the prompt you provide a demonstration of the solution. In this case we provide examples of the input and also requirements for the output. This is a good way to instruct Copilot to generate specific solutions.

- Copilot will suggest a new `RideLog` record type and a `Parse` method. The `Parse` method splits the string and assigns each part to a corresponding property.

    ```csharp
   using System;

   namespace BikeShopAPI.Entities
   {
       public record RideLog(DateTime Date, string StartLocation, string EndLocation, string RouteName)
       {
        public static RideLog Parse(string rideLogSignatures)
           {
            var parts = rideLogSignatures.Split('-');
            if (parts.Length != 4)
            {
                throw new FormatException("Invalid ride log signature format.");
            }

            var date = DateTime.ParseExact(parts[0], "ddMMyyyy", null);
            var startLocation = parts[1];
            var endLocation = parts[2];
            var routeName = parts[3];

            return new RideLog(date, startLocation, endLocation, routeName);
           }
       }
    }
    ```

    A C# record type is a reference type that provides built-in functionality for encapsulating data. It is a reference type that is similar to a class, but it is immutable by default. It is a good choice for a simple data container.

    GitHub Copilot is very good at understanding the context of the code. From the prompt we gave it, it understood that the `RideLogSignatures` is a list of strings in a specific format and that it can be parsed into a `RideLogSignature` model, to make the code more readable and maintainable.

- In GitHub Copilot Chat, click the ellipses `...` and select `Insert into New File` for the suggested `RideLog` record as `BikeShopAPI/Entities/RideLog.cs`.

    ![ride-log-signature](../../img/lab-3-4-ride-log-signature.png)

    GitHub Copilot has many quick actions that can be used to speed up the development process. In this case, it created a new file based on the code suggestions.

- Copilot will add the code to a new empty file, but must be saved.

- Save the file by clicking pressing `Ctrl + S` or `Cmd + S`.

- Navigate to folder `/BikeShopAPI/Entities` and save the file as `RideLog.cs`.

- Now, let's add the new `RideLogs` property to the `Bike` model.

- Open the `Entities/Bike.cs` file.

- Add the `RideLogs` property to the `Bike` model

    ```csharp
    public class Bike
    {
        // Other properties
        // ...

        // New properties
        public List<string> RideLogSignatures { get; set; } = new List<string>();
        public List<RideLog> RideLogs => RideLogSignatures.Select(RideLog.Parse).ToList();
    }
    ```

- Let's add both `RideLogSignatures` and `RideLogs` to `var bikeDetails` in `GetBikeDetails` in `BikeShopController.cs`:
  
    ```csharp
    var bikeDetails = new
    {
      //  ...
      RideLogSignatures = bike.RideLogSignatures,
      RideLogs = bike.RideLogs, 
    }
    ```

- Again select all content of the BikeShops List, press `ctrl + i` to start `inline chat` and type the following command

    ```text
    Add RideLogSignature example to each Bike, examples should follow this format: "17091908-DEP-ARR-WB004", each bike should have at least two signatures
    ```

   ![http-post-shop](../../img/lab-3-4-ride-log-examples.png)

- Now, run the app and test the new functionality.

    ```bash
    cd src/BikeShopAPI
    dotnet run
    ```

- Open `BikeShopAPI/examples/Shops.http` file in the Visual Studio code IDE and execute the GET request to see details of a bike shop.

- Click the `Send Request` button for the `GET` request:

    ```text
    GET https://localhost:1903/api/bikeshop/1/bikes/1/details
    ```

    ![http-get-bikedetails](../../img/lab-3-5.png)

- The Rest Client response will now include the `RideLog` property as follows:

    ```text
    HTTP/1.1 200 OK
    Connection: close
    {
        "id": 1,
        "model": "Synapse",
        "brand": "Cannondale",
        "description": "A road bike that's light, stiff, fast and surprisingly comfortable.",
        "price": 2000,
        "rearTravel": 0,
        "forkTravel": 0,
        "waterInBidon": 400,
        "bikeShopId": 1,
        "name": "Fast Wheels",
        "rideLogSignatures": [
            "17091908-DEP-ARR-WB001",
            "17091909-DEP-ARR-WB001"
        ],
        "rideLogs": [
            {
                "date": "1908-09-17T00:00:00",
                "startLocation": "DEP",
                "endLocation": "ARR",
                "routeName": "WB001"
            },
            {
                "date": "1909-09-17T00:00:00",
                "startLocation": "DEP",
                "endLocation": "ARR",
                "routeName": "WB001"
            }
        ]
    }
    ```

- Note the `rideLogs` property that is parsed based on the `RideLogSignatures`

- Stop the app by pressing `Ctrl + C`in the terminal.

### Step 5: Advanced Prompt Engineering

- Open the `Bike.cs` file.

- Add a new `BikeTrickSignatures` property of type string to the `Bike.cs` file.

    ```csharp
    public class Bike
    {
        
        // Other properties

        // New property
        public List<string> BikeTrickSignatures { get; set; } = new List<string>();
    }

    ```

- The `BikeTrickSignatures` is a fictional property that is used to demonstrate the capabilities of GitHub Copilot. It is not a real cycling concept.

- Some examples of a `BikeTrickSignatures` are:
  - L4B-H2C-R3A-S1D-T2E
  - L1A-H1B-R1C-T1E
  - L2A-H2B-R2C

- Let's prompt engineer Copilot to generate a solution for the `BikeTrickSignatures` property.

- Open GitHub Copilot Chat, click **+** to clear prompt history, then ask the question:

    ```text
    Parse a BikeTrickSignatures property into a C# model.

    ## BikeTrickSequences Examples
    L4B-H2C-R3A-S1D-T2E
    L1A-H1B-R1C-T1E
    L2A-H2B-R2C

    ## Trick
    Actions: L = 360, H = Tuck No-Hander, R = Cash Roll, S = Barspin, T = Table
    Number indicates repetition count
    The Letter represents difficulty (A-E)
    Difficulty modifiers: A = 1.0, B = 1.2, C = 1.4, D = 1.6, E = 1.8
    
    ## Bike Trick Difficulty Method
    Implement a difficulty calculation method with the following rules:
    A Cash Roll after a 360 is scored double
    A Barspin after a Table is scored triple
    
    ## Thought Process
    Example: L4B-R3A-H2C-T2E-S1D

    360: 4 * 1.2 = 4.8
    Cash Roll: 3 * 1 * 2 (Cash Roll a 360) = 6.0
    Tuck No-Hander: 2 * 1.4 = 2.8
    Table: 2 * 1.8 = 3.6
    Barspin: 1 * 1.6 * 3 (Barspin after a Table) = 4.8
    Total Difficulty: 22

    ## Technical Specifications
    - Develop a BikeTrickSequence class with a collection of Tricks and a difficulty attribute
    Introduce the Trick class within the BikeTrickSequence class
    Use a static Parse method for interpreting the BikeTrickSignature
    Interpret the signature with a Regex
    Import the relevant modules to the beginning of the file
    Adjust the difficulty outcome to two decimal places

    Let's think step by step.
    ```

    Sometimes the Copilot doens't complete the output of the prompt. Make sure to try the prompt again if you are not successful the first time.

- Note the `Chain-of-Thought reasoning` heading. In this case we add reasoning about how a difficulty is calculated based on the sequence of tricks.

    Chain-of-Thought (CoT) prompting enables complex reasoning capabilities through intermediate reasoning steps. You can combine it with Few-Shot prompting to get better results on more complex tasks that require reasoning before responding.

- Note the `Let's think step by step.` at the end of the prompt engineering. This is the final instruction for Chain-of-Thought to make Copilot go through the process step by step, like a human would do.

- Also note the Few-Shot examples in the `BikeTrickSequence Examples` heading. Chain-of-Thought with Few-Shot combined is the currently the most powerful way to instruct Copilot to generate specific and accurate solutions. Still it doesn't guarantee that Copilot will generate the correct solution. That will require fine-tuning the prompt (prompt engineering).

- Copilot will output something that looks as follows after inserting the prompt above in Copilot Chat:

    ```text
        1. Create a Trick class with properties for Action, RepetitionCount, DifficultyModifier, and Score.
        2. Create a BikeTrickSequence class with a List<Trick> and a Difficulty attribute.
        3. In the BikeTrickSequence class, create a static Parse method that takes a BikeTrickSignature string as input.
        4. In the Parse method, use a regular expression to split the input string into individual tricks.
        5. For each trick, parse the action, repetition count, and difficulty modifier.
        6. Create a new Trick object with the parsed values and add it to the BikeTrickSequence's list.
        7. Calculate the score for each trick based on the rules provided and add it to the Difficulty attribute.
        8. Round the Difficulty attribute to two decimal places.
        9. Return the BikeTrickSequence object.
    ```

- This is the output of the Chain-of-Thought reasoning. It is a step by step guide to create the `BikeTrickSequence` class. Copilot is thinking step by step, like a human would do.

- Copilot will also suggest a `BikeTrickSequence` class with an implementation that looks like this. The result varies, but the output should be a `BikeTrickSequence` class with a `Trick` class, a `Parse` method and a `CalculateDifficulty` method.

    > The required file already exists. If your output does not align with the provided example, feel free to utilize this existing file.
    >
    > Sometimes, instead of generating a class, Copilot will suggest doing it in the next step. You can click the suggestion to proceed.
    >
    > ![Class generation](../../img/lab-3-5-class-generate.png)

    ```csharp
    using System.Text.RegularExpressions;

    public class BikeTrickSequence
    {
       public List<Trick> Tricks { get; set; } = new List<Trick>();
       public double TotalDifficulty => CalculateTotalDifficulty();
    }
       public static BikeTrickSequence Parse(string signature)
       {
          var sequence = new BikeTrickSequence();
          var regex = new Regex(@"([LHRST])(\d)([A-E])");
          var matches = regex.Matches(signature);

          foreach (Match match in matches)
          {
             var action = match.Groups[1].Value;
             var repetition = int.Parse(match.Groups[2].Value);
             var difficultyModifier = GetDifficultyModifier(match.Groups[3].Value);

             sequence.Tricks.Add(new Trick
             {
                Action = action,
                Repetition = repetition,
                DifficultyModifier = difficultyModifier
             });
           }

           return sequence;
        }

        private static double GetDifficultyModifier(string difficulty)
        {
           return difficulty switch
           {
             "A" => 1.0,
             "B" => 1.2,
             "C" => 1.4,
             "D" => 1.6,
             "E" => 1.8,
              _ => throw new ArgumentException("Invalid difficulty level")
           };
        }

        private double CalculateTotalDifficulty()
        {
           double total = 0.0;

           for (int i = 0; i < Tricks.Count; i++)
           {
             var trick = Tricks[i];
             double baseDifficulty = trick.Repetition * trick.DifficultyModifier;

            // Apply special rules
           if (i > 0)
           {
             var previousTrick = Tricks[i - 1];
             if (trick.Action == "R" && previousTrick.Action == "L") // Cash Roll after 360
             {
                baseDifficulty *= 2;
             }
             else if (trick.Action == "S" && previousTrick.Action == "T") // Barspin after Table
             {
                baseDifficulty *= 3;
             }
           }

            total += baseDifficulty;
        }

        return Math.Round(total, 2); // Round to 2 decimal places
    }

    public class Trick
    {
        public string Action { get; set; } = string.Empty;
        public int Repetition { get; set; }
        public double DifficultyModifier { get; set; }
    }

    ```

- In GitHub Copilot Chat, click the ellipses `...` and select `Insert into New File` for the suggested `BikeTrickSequence` class as `BikeShopAPI/Entities/BikeTrickSequence.cs`.

    ![bike-manouver-sequence-signature](../../img/lab-3-6-bike-trick-sequence-signature.png)

- Copilot will add the code to a new empty file, but must be saved.

- Save the file by clicking pressing `Ctrl + S` or `Cmd + S`.

- Navigate to folder `/BikeShopAPI/Entities` and save the file as `BikeTrickSequence.cs`.

- Now, let's add the new `BikeTrickSequence` property to the `Bike` model.

- Open the `Entities/Bike.cs` file.

- Add the `BikeTrickSequence` property to the `Bike` model.

    ```csharp
    public class Bike
    {
        // Other properties
        public List<string> BikeTrickSignatures { get; set; } = new List<string>();
        // New property
        public List<BikeTrickSequence> BikeTricks => BikeTrickSignatures.Select(BikeTrickSequence.Parse).ToList();
    }
    ```

- Let's add `BikeTrickSignatures` and `BikeTricks` to `var bikeDetails` in `GetBikeDetails` method:

    ```csharp
    var bikeDetails = new
    {
        // ...
        BikeTrickSignatures = bike.BikeTrickSignatures,
        BikeTricks = bike.BikeTricks
    }
    ```

- Again select all content of the BikeShops List, press `ctrl + i` to start `inline chat` and type the following command:

    ```text
    Add BikeTrickSignature example to each Bike, examples should follow this format: "L4B-H2C-R3A-S1D-T2E", each bike should have at least two signatures
    ```

   ![http-post-shop](../../img/lab-3-5-trick-examples.png)

- Now, run the app and test the new functionality.

    ```bash
    cd src/BikeShopAPI
    dotnet run
    ```

- Open `BikeShopAPI/examples/Shops.http` file in the Visual Studio code IDE and click the `Send Request` button for the `GET` below:

    ![http-post-shop](../../img/lab-3-5.png)

- The Rest Client response will now include the `BikeTrickSequence` property as follows:

    ```text
    HTTP/1.1 200 OK
    Connection: close
    {
        "id": 1,
        "model": "Synapse",
        "brand": "Cannondale",
        "description": "A road bike that's light, stiff, fast and surprisingly comfortable.",
        "price": 2000,
        "rearTravel": 0,
        "forkTravel": 0,
        "waterInBidon": 400,
        "bikeShopId": 1,
        "name": "Fast Wheels",
        "rideLogSignatures": [
            "17091908-DEP-ARR-WB001",
            "17091909-DEP-ARR-WB001"
        ],
        "rideLogs": [
            {
                "date": "1908-09-17T00:00:00",
                "startLocation": "DEP",
                "endLocation": "ARR",
                "routeName": "WB001"
            },
            {
                "date": "1909-09-17T00:00:00",
                "startLocation": "DEP",
                "endLocation": "ARR",
                "routeName": "WB001"
            }
        ],
        "bikeTrickSignatures": [
            "L4B-H2C-R3A-S1D-T2E",
            "L4B-H2C"
        ],
        "bikeTrick": [
            {
                "tricks": [
                    {
                        "action": "L",
                        "count": 4,
                        "difficultyModifier": "B",
                        "modifierValue": 1.2
                    },
                    {
                        "action": "H",
                        "count": 2,
                        "difficultyModifier": "C",
                        "modifierValue": 1.4
                    },
                    {
                        "action": "R",
                        "count": 3,
                        "difficultyModifier": "A",
                        "modifierValue": 1
                    },
                    {
                        "action": "S",
                        "count": 1,
                        "difficultyModifier": "D",
                        "modifierValue": 1.6
                    },
                    {
                        "action": "T",
                        "count": 2,
                        "difficultyModifier": "E",
                        "modifierValue": 1.8
                    }
                ],
                "difficulty": 15.8
            },
            {
                "tricks": [
                    {
                        "action": "L",
                        "count": 4,
                        "difficultyModifier": "B",
                        "modifierValue": 1.2
                    },
                    {
                        "action": "H",
                        "count": 2,
                        "difficultyModifier": "C",
                        "modifierValue": 1.4
                    }
                ],
                "difficulty": 7.6
            }
        ]
    }
    ```

- Note the `bikeTrickSequences` property that is parsed based on the `bikeTrickSignatures`.

- Stop the app by pressing `Ctrl + C` in the terminal.

### Step 6: Building a Migration Plan to a New Programming Language

In this advanced scenario, you’ll use Copilot’s reasoning and documentation abilities to craft a migration plan for moving your project to a new programming language (e.g., from C# to Java, Python, or Go).
This is a multi-stage process, leveraging Copilot’s ability to generate and refine structured documentation.

#### 1. Generate a High-Level Migration Strategy

- Open Copilot Chat in the **Agent** mode and prompt:

    ```text
    I want to migrate my project from C# to Python.
    Generate a high-level migration plan as a Markdown file added to repo
    Focus on goals, main challenges, and core migration phases.
    ```

- Review Copilot’s outline. It should cover:
  - Migration objectives (why and what to migrate)
  - Key risks and challenges (e.g., language features, dependencies, team skills)
  - Major phases (assessment, planning, conversion, validation, deployment)

  ![prompt](../../img/lab3step6-1.png)
  ![file](../../img/lab3step6-2.png)

#### 2. Expand Each Migration Phase into Actionable Steps

- In Copilot Chat, use the outline from the previous step and prompt:

    ```text
    For each phase in the migration plan, provide detailed, actionable steps.  
    Include team roles, required tools, code audit methods, and key checkpoints.
    Generate a new markdown file named "MigrationPlanDetailed.md".
    ```

- Expect Copilot to break down each phase into specific tasks, such as:
  - Inventory current codebase and dependencies
  - Identify language-specific constructs and equivalents
  - Set up the new project structure in Python
  - Plan automated and manual testing strategies

     ![Prompt](../../img/lab3step6-3.png)
     ![File](../../img/lab3step6-4.png)

#### 3. Identify Compatibility, Testing, and Deployment Requirements

- In Copilot Chat, prompt:

    ```text
    What compatibility issues should I watch out for when migrating from C# to Python?  
    List strategies for automated testing, CI/CD adaptation, and deployment in the new environment.
    Save it as MigrationDevOps markdown file.
    ```

- Copilot should provide:
  - Common pitfalls (e.g., data types, threading, library differences)
  - Testing strategies (unit, integration, regression tests)
  - Suggestions for updating pipelines/deployment scripts
  - Tooling recommendations for the target language

     ![Prompt](../../img/lab3step6-5.png)
     ![File](../../img/lab3step6-6.png)

Now we have three .md files. We can keep them seperate or ask Copilot to combine them into one file called "MigrationFullPlan.md":

- In Copilot Chat, prompt:

    ```text
    Combine all three markdown files (MigrationDevops.md, MigrationPlan.md and MigrationPlanDetailed.md) into one file called "MigrationFullPlan.md".
    After that remove MigrationDevops.md, MigrationPlan.md and MigrationPlanDetailed.md and only keep MigrationFullPlan.md.
    ```

   ![Prompt](../../img/lab3step6-7.png)
   ![File](../../img/lab3step6-8.png)

Now we will have to execute the command provided by Copilot by clicking **Continue**

   ![File](../../img/lab3step6-9.png)

Apply this for all three files. At the end we will only have **MigrationFullPlan.md** file
