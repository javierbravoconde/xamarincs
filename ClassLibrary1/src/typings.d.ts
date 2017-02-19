// Typings reference file, you can add your own global typings here
// https://www.typescriptlang.org/docs/handbook/writing-declaration-files.html

declare var System: any;

declare interface CSharpInterface {
    processEvent(eventStr: string);
}

declare var CSharp: CSharpInterface;
