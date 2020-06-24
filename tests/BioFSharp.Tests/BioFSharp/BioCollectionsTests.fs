﻿namespace BioFSharp.Tests

open Expecto
open BioFSharp

module BioCollectionsTests =
    
    open AminoAcids
    
    let aminoAcidSetArray : BioArray.BioArray<AminoAcid> = 
        [|Ala;Cys;Asp;Glu;Phe;Gly;His;Ile;Lys;Leu;Met;Asn;Pyl;Pro;Gln;Arg;Ser;Thr;Sel;Val;Trp;Tyr;Xaa;Xle;Glx;Asx;Gap;Ter|]

    let aminoAcidSymbolSetArray : BioArray.BioArray<AminoAcidSymbols.AminoAcidSymbol> = 
        aminoAcidSetArray |> Array.map AminoAcidSymbols.aminoAcidSymbol

    let testProt : BioArray.BioArray<AminoAcid> = 
        [|Met;Val;Leu|]

    open Nucleotides

    let nucleotideSetArray : BioArray.BioArray<Nucleotide> = 
        [|A;T;G;C;U;I;Gap;Ter;R;Y;K;M;S;W;B;D;H;V;N|]

    let testCodingStrand : BioArray.BioArray<Nucleotides.Nucleotide> = 
        [|A;T;G;G;T;A;C;T;G;A;C|]

    let testCodingStrandRev : BioArray.BioArray<Nucleotides.Nucleotide> = 
        testCodingStrand |> Array.rev

    let testCodingStrandRevComplement : BioArray.BioArray<Nucleotides.Nucleotide> = 
        [|G;T;C;A;G;T;A;C;C;A;T|]  

    let testTemplateStrand : BioArray.BioArray<Nucleotides.Nucleotide> = 
        [|T;A;C;C;A;T;G;A;C;T;G|]

    let testTranscript : BioArray.BioArray<Nucleotides.Nucleotide> = 
        [|A;U;G;G;U;A;C;U;G;A;C|]

    let testTriplets =
        [|(T,A,C);(C,A,T);(G,A,C)|]

    [<Tests>]
    let bioCollectionsTests  =
        
        testList "BioCollections" [

            testList "BioArray" [
            

                testCase "ofAminoAcidString" (fun () ->
                    let parsedAminoAcids =
                        "ACDEFGHIKLMNOPQRSTUVWYXJZB-*"
                        |> BioArray.ofAminoAcidString

                    Expect.equal
                        aminoAcidSetArray
                        parsedAminoAcids
                        "BioArray.ofAminoAcidString did not parse the amino acid set correctly."
                )

                testCase "ofAminoAcidSymbolString" (fun () ->
                    let parsedAminoAcidSymbols =
                        "ACDEFGHIKLMNOPQRSTUVWYXJZB-*"
                        |> BioArray.ofAminoAcidSymbolString

                    Expect.equal 
                        aminoAcidSymbolSetArray
                        parsedAminoAcidSymbols
                        "BioArray.ofAminoAcidSymbolString did not parse the amino acid set correctly."
                )

                testCase "ofNucleotideString" (fun () ->
                    let parsedNucleotides =
                        "ATGCUI-*RYKMSWBDHVN"
                        |> BioArray.ofNucleotideString

                    Expect.equal 
                        nucleotideSetArray
                        parsedNucleotides
                        "BioArray.ofNucleotideString did not parse the nucleotide set correctly."
                )

                testCase "reverse" (fun () ->
                    Expect.equal 
                        (testCodingStrand |> BioArray.reverse)
                        testCodingStrandRev
                        "BioArray.reverse did not reverse the nucleotide sequence correctly."
                )
            
                testCase "complement" (fun () ->
                    Expect.equal 
                        (testCodingStrand |> BioArray.complement)
                        testTemplateStrand
                        "BioArray.complement did not build the reverse complement of the nucleotide sequence correctly."
                )

                testCase "reverseComplement" (fun () ->
                    Expect.equal 
                        (testCodingStrand |> BioArray.reverseComplement)
                        testCodingStrandRevComplement
                        "BioArray.reverseComplement did not build the reverse complement of the nucleotide sequence correctly."
                )

                testCase "mapInTriplets" (fun () ->
                    Expect.equal 
                        (testTemplateStrand |> BioArray.mapInTriplets id)
                        testTriplets
                        "BioArray.reverseComplement did not build the correct base triplets."
                )

                testCase "transcribeCodeingStrand" (fun () ->
                    Expect.equal 
                        (testCodingStrand |> BioArray.transcribeCodingStrand)
                        testTranscript
                        "BioArray.transcribeCodeingStrand did not transcribe the coding strand correctly."
                )

                testCase "transcribeTemplateStrand" (fun () ->
                    Expect.equal 
                        (testTemplateStrand |> BioArray.transcribeTemplateStrand)
                        testTranscript
                        "BioArray.transcribeTemplateStrand did not transcribe the template strand correctly."
                )

                testCase "translate" (fun () ->
                    Expect.equal 
                        (testTranscript |> BioArray.translate 0)
                        testProt
                        "BioArray.translate did not translate the transcript correctly."
                )

            ]

            testList "BioList" [
            

                testCase "ofAminoAcidString" (fun () ->
                    let parsedAminoAcids =
                        "ACDEFGHIKLMNOPQRSTUVWYXJZB-*"
                        |> BioList.ofAminoAcidString

                    Expect.equal
                        parsedAminoAcids
                        (aminoAcidSetArray|> List.ofArray)
                        "BioList.ofAminoAcidString did not parse the amino acid set correctly."
                )

                testCase "ofAminoAcidSymbolString" (fun () ->
                    let parsedAminoAcidSymbols =
                        "ACDEFGHIKLMNOPQRSTUVWYXJZB-*"
                        |> BioList.ofAminoAcidSymbolString

                    Expect.equal 
                        (aminoAcidSymbolSetArray |> List.ofArray)
                        parsedAminoAcidSymbols
                        "BioList.ofAminoAcidSymbolString did not parse the amino acid set correctly."
                )

                testCase "ofNucleotideString" (fun () ->
                    let parsedNucleotides =
                        "ATGCUI-*RYKMSWBDHVN"
                        |> BioList.ofNucleotideString

                    Expect.equal 
                        (nucleotideSetArray |> List.ofArray)
                        parsedNucleotides
                        "BioList.ofNucleotideString did not parse the nucleotide set correctly."
                )

                testCase "reverse" (fun () ->
                    Expect.equal 
                        (testCodingStrand |> List.ofArray |> BioList.reverse)
                        (testCodingStrandRev |> List.ofArray)
                        "BioList.reverse did not reverse the nucleotide sequence correctly."
                )
            
                testCase "complement" (fun () ->
                    Expect.equal 
                        (testCodingStrand |> BioList.ofBioArray |> BioList.complement)
                        (testTemplateStrand |> List.ofArray)
                        "BioList.complement did not build the reverse complement of the nucleotide sequence correctly."
                )

                testCase "reverseComplement" (fun () ->
                    Expect.equal 
                        (testCodingStrand |> BioList.ofBioArray |> BioList.reverseComplement)
                        (testCodingStrandRevComplement |> List.ofArray)
                        "BioList.reverseComplement did not build the reverse complement of the nucleotide sequence correctly."
                )

                testCase "mapInTriplets" (fun () ->
                    Expect.equal 
                        (testTemplateStrand |> BioList.ofBioArray |> BioList.mapInTriplets id)
                        (testTriplets |> List.ofArray)
                        "BioList.reverseComplement did not build the correct base triplets."
                )

                testCase "transcribeCodeingStrand" (fun () ->
                    Expect.equal 
                        (testCodingStrand |> BioList.ofBioArray |> BioList.transcribeCodingStrand)
                        (testTranscript |> List.ofArray)
                        "BioList.transcribeCodeingStrand did not transcribe the coding strand correctly."
                )

                testCase "transcribeTemplateStrand" (fun () ->
                    Expect.equal 
                        (testTemplateStrand |> BioList.ofBioArray |> BioList.transcribeTemplateStrand)
                        (testTranscript |> List.ofArray)
                        "BioList.transcribeTemplateStrand did not transcribe the template strand correctly."
                )

                testCase "translate" (fun () ->
                    Expect.equal 
                        (testTranscript |> BioList.ofBioArray |> BioList.translate 0)
                        (testProt |> List.ofArray)
                        "BioList.translate did not translate the transcript correctly."
                )

            ]

            testList "BioSeq" [
            

                    testCase "ofAminoAcidString" (fun () ->
                        let parsedAminoAcids =
                            "ACDEFGHIKLMNOPQRSTUVWYXJZB-*"
                            |> BioSeq.ofAminoAcidString

                        Expect.sequenceEqual
                            parsedAminoAcids
                            (aminoAcidSetArray|> Seq.ofArray)
                            "BioSeq.ofAminoAcidString did not parse the amino acid set correctly."
                    )

                    testCase "ofAminoAcidSymbolString" (fun () ->
                        let parsedAminoAcidSymbols =
                            "ACDEFGHIKLMNOPQRSTUVWYXJZB-*"
                            |> BioSeq.ofAminoAcidSymbolString

                        Expect.sequenceEqual 
                            (aminoAcidSymbolSetArray |> Seq.ofArray)
                            parsedAminoAcidSymbols
                            "BioSeq.ofAminoAcidSymbolString did not parse the amino acid set correctly."
                    )

                    testCase "ofNucleotideString" (fun () ->
                        let parsedNucleotides =
                            "ATGCUI-*RYKMSWBDHVN"
                            |> BioSeq.ofNucleotideString

                        Expect.sequenceEqual 
                            (nucleotideSetArray |> Seq.ofArray)
                            parsedNucleotides
                            "BioSeq.ofNucleotideString did not parse the nucleotide set correctly."
                    )

                    testCase "reverse" (fun () ->
                        Expect.sequenceEqual 
                            (testCodingStrand |> Seq.ofArray |> BioSeq.reverse)
                            (testCodingStrandRev |> Seq.ofArray)
                            "BioSeq.reverse did not reverse the nucleotide sequence correctly."
                    )
            
                    testCase "complement" (fun () ->
                        Expect.sequenceEqual 
                            (testCodingStrand |> BioSeq.ofBioArray |> BioSeq.complement)
                            (testTemplateStrand |> Seq.ofArray)
                            "BioSeq.complement did not build the reverse complement of the nucleotide sequence correctly."
                    )

                    testCase "reverseComplement" (fun () ->
                        Expect.sequenceEqual 
                            (testCodingStrand |> BioSeq.ofBioArray |> BioSeq.reverseComplement)
                            (testCodingStrandRevComplement |> Seq.ofArray)
                            "BioSeq.reverseComplement did not build the reverse complement of the nucleotide sequence correctly."
                    )

                    testCase "mapInTriplets" (fun () ->
                        Expect.sequenceEqual 
                            (testTemplateStrand |> BioSeq.ofBioArray |> BioSeq.mapInTriplets id)
                            (testTriplets |> Seq.ofArray)
                            "BioSeq.reverseComplement did not build the correct base triplets."
                    )

                    testCase "transcribeCodeingStrand" (fun () ->
                        Expect.sequenceEqual 
                            (testCodingStrand |> BioSeq.ofBioArray |> BioSeq.transcribeCodingStrand)
                            (testTranscript |> Seq.ofArray)
                            "BioSeq.transcribeCodeingStrand did not transcribe the coding strand correctly."
                    )

                    testCase "transcribeTemplateStrand" (fun () ->
                        Expect.sequenceEqual
                            (testTemplateStrand |> BioSeq.ofBioArray |> BioSeq.transcribeTemplateStrand)
                            (testTranscript |> Seq.ofArray)
                            "BioSeq.transcribeTemplateStrand did not transcribe the template strand correctly."
                    )

                    testCase "translate" (fun () ->
                        Expect.sequenceEqual 
                            (testTranscript |> BioSeq.ofBioArray |> BioSeq.translate 0)
                            (testProt |> Seq.ofArray)
                            "BioSeq.translate did not translate the transcript correctly."
                    )

            ]
        ]