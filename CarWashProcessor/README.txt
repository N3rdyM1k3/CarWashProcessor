SETUP
This is a car washing application that washes cars. The two features are washes and add-ons. The system was built as a prototype and works well, but the complexity is already adding up. Additionally, many more washes and add-ons are expected to be brought on over time.

RULES
Your work starts the CarJobProcessorService.cs entry point. Do not modify:
- Worker.cs
- CarJob.cs
- CarJobService.cs

You may need to modify Program.cs but do not modify the main method.

PROMPT
Refactor the code for this system so that:
- Once refactored, adding new washes and add-ons should be as easy and safe as possible
  - Ideally, other developers should not have to have advanced knowledge of the inner workings
  - Ideally, adding new washes and add-ons should not require adding/editing many files
  - Ideally, adding to and using the system is intuitive and self-explanatory through architecture to other developers
- Once refactored, the underlying architecture is well-designed and clear
  - Ideally, the underlying architecture should be able to be changed without fundamentally changing the already implemented washes and add-ons
  - Ideally, other developers with advanced knowledge should be able to understand the inner workings
- The system is reasonably performant

QUESTIONS
Please ask questions if you're at all unclear on the goal and what is being asked.

TIP
This challenge is about code organization, stability, and longevity. It's not about cloud architecture (databases, queues, etc.).