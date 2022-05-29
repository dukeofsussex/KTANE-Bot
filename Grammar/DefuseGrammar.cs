using System.IO;
using System.Speech.Recognition;

namespace KTANE_Bot
{
    public static class DefuseGrammar
    {
        public static Grammar StandardDefuseGrammar = new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"Defuse.txt"))));
        public static Grammar BombCheckGrammar => _BombCheckGrammar();
        public static Grammar ButtonGrammar => _ButtonGrammar();
        public static Grammar MemoryGrammar => _MemoryGrammar();
        public static Grammar WiresGrammar => _WiresGrammar();
        public static Grammar SymbolsGrammar => _SymbolsGrammar();
        
        
        //bomb checking grammar
        private static Grammar _BombCheckGrammar()
        {
            var batteryChoices = new Choices(new string[] {"none", "0", "1", "2", "more than 2", "3", "4", "5", "6"});
            var countBatteries = new GrammarBuilder(batteryChoices);
            var trueOrFalse = new Choices(new string[] {"yes", "no", "true", "false"});
            var oddEven = new Choices(new string[] {"odd", "even"});
            
            //batteries
            var battery = new GrammarBuilder("Batteries");
            battery.Append(countBatteries);
                
            //parallel port
            var parallelPort = new GrammarBuilder("Parallel");
            parallelPort.Append(trueOrFalse);
                
            //frk, interpreted as the word "freak".
            var frk = new GrammarBuilder("Freak");
            frk.Append(trueOrFalse);
                
            //car, interpreted as the word "car".
            var car = new GrammarBuilder("Car");
            car.Append(trueOrFalse);
                
            //vowel in serial number
            var vowel = new GrammarBuilder("Vowel");
            vowel.Append(trueOrFalse);
                
            //last number of serial number
            var digit = new GrammarBuilder("Digit");
            digit.Append(oddEven);
                
            var allChoices = new Choices(new GrammarBuilder[] {battery, vowel, parallelPort, digit, frk, car});
            return new Grammar(allChoices);
        }

        private static Grammar _ButtonGrammar()
        {
            var labelChoices = new Choices(new string[] { "detonate", "hold", "press", "abort", "stripe"});
            var red = new GrammarBuilder("Red");
            var yellow = new GrammarBuilder("Yellow");
            var blue = new GrammarBuilder("Blue");
            var white = new GrammarBuilder("White");
            
            red.Append(labelChoices);
            yellow.Append(labelChoices);
            blue.Append(labelChoices);
            white.Append(labelChoices);

            var allChoices = new Choices(new GrammarBuilder[] {red, yellow, blue, white});
            return new Grammar(allChoices);
        }

        private static Grammar _MemoryGrammar()
        {
            var nums = new Choices(new string[] {"1", "2", "3", "4"});
            var sequence = new GrammarBuilder("Numbers");
            sequence.Append(nums);

            var allChoices = new Choices(sequence);
            return new Grammar(allChoices);
        }

        private static Grammar _WiresGrammar()
        {
            return new Grammar(new GrammarBuilder(new Choices(new string[] {"yellow", "blue", "black", "white", "red", "done"})));
        }

        private static Grammar _SymbolsGrammar()
        {
            var firstColumn = new Choices("capital queue", "capital ay", "lambda", "lightning", "kitty", "kappa", "reverse dotted c");
            var secondColumn = new Choices("e with umlaut", "capital queue", "reverse dotted c", "snake", "empty star",
                "kappa", "question mark");
            var thirdColumn = new Choices("copyright", "omega", "snake", "ex eye", "three", "lambda", "empty star");
            var fourthColumn = new Choices("six", "paragraph", "tampa bay", "kitty", "ex eye", "question mark",
                "smiley face");
            var fifthColumn = new Choices("psi", "smiley face", "tampa bay", "dotted c", "paragraph", "evil three",
                "full star");
            var sixthColumn = new Choices("six", "e with umlaut", "dumbbell", "ay ee", "psi", "reverse n",
                "capital omega");

            var builder = new GrammarBuilder();
            builder.Append(firstColumn);
            builder.Append(secondColumn);
            builder.Append(thirdColumn);
            builder.Append(fourthColumn);
            builder.Append(fifthColumn);
            builder.Append(sixthColumn);

            return new Grammar(builder);
        }
    }
}