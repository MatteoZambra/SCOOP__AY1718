
	----------------------------------------------------
	Scientific Computing and Object Oriented Programming
	Mathematical Engineering
	Final Project - A.A. 2017/2018
	Instructors: Di Buccio, E., Schimd, M.
	
	Students:
		Vittoria Lazzarotto :: ID: 1180128
		Matteo   Zambra     :: ID: 1140056
		
	----------------------------------------------------
	
	
	
	1. Directories
	
	The present file lies in C:\...\SCOOP_FinalProject_VittoriaLazzarotto_MatteoZambra\
	Here are present two folders
		
		Report: containing the .pdf report file
		
		MLProj, which contains
		
			- InOut         :: WHICH SHOULD NOT BE DELETED/MODIFIED
					           and contains
					         
					        	- Source: which contains input files
					        	
					        	- Drop  : which IS NOW EMPTY, but other folders will be created
					        	          during the execution of the program
					        			  
					        	IMPORTANT: Source\ and Drop\ must NOT be modified/deleted
					        			
			- MLProj2018    :: which contains 
			
								- Microsoft Visual Studio 2017 Solution file MLProj2018.sln
								  that can be launched and the execution of the program from 
								  there started
								  
								- MLProj2018 Visual Studio Project directory, containing:
								
									- bin folder, then
									
										- Debug folder where the MLProj2018.exe lies
									  
									- source code
									
									- MLProj2018.csproj
									
									- configuration files
									
									
	
	2. External libraries and dependencies
	
	The correct run needs the following installations (as is given, in MLProj2018.sln have been left ONLY THOSE REFERENCES
	which are present already in a new Visual Studio Solution)
	
		- Accord.3.8.0						(manually installed)
		
		- Accord.Controls.3.8.0					(manually installed)
		
		- Accord.Extensions.Core.3.0.1
		
		- Accord.MachineLearning.3.8.0
		
		- Accord.LachineLearning.GPL.3.8.0			(manually installed)
		
		- Accord.IO.3.8.0					(manually installed)
		
		- Accord.Math.3.8.0
		
		- Accord.Statistics.3.8.0
		
	Two more are eventually automatically installed 
	
		- SharpZipLib.0.86.0
		
		- ZedGraph.5.1.7
		
	these latter two are not to be manually installed.
	The above listed libraries are NECESSARY and sufficient for a correct program run.

	
	
	3. Program usage, run and behaviour
	
	Once dependencies installed (and folders as described in 1. and not touched), the program 
	should dispose all the necessary.
	It is possible to run it by both
	
		- launching the executable MLProj2018.exe in C:\...\MLProj2018\MLProj2018\bin\Debug\
		
		- opening MLProj2018.sln and running the program
		
	Thus doing, the program automatically searches the directory InOut, enters Source to grab the 
	user-defined input file and enters Drop to set the drop point for summarising report files generated
	(this is achieved creating the proper folders according to the keyboard inputs of the user). 
	
	The program asks the user to choose 
	
		- the data set (whether Red Wines Quality, Biomechanical features, Iris data set, see the Report file 
		  for a more extensive expaination)
		  
		- whether the data want to be preprocessed and 
		
			- if not, the program ignores preprocessing
			
			- if yes, a window pops up, that is the scree plot, on the basis of which the 
			  users is asked how many principal components keep. The next input choice can 
			  be inserted even if the mentioned window is not closed. 
			  
			  Then another window opens, that is the data scatterplot in the two dimensional
			  reduced space (if the user chose two PCs). Again the rest of the program run is
			  not dependent on whether these windows stay open or are closed.
			  
		- Test set and train set partitioning (if 0.1 and 0.9 or 1/3 and 2/3 of the total data set).
		
		- How many runs perform. If 1 is entered, the learning procedure (for each of the algorithms
		  used) is done once. If > 1 is entered, the learning procedure is repeated the number inputed
		  (in this case, the previously mentioned choices are reused). This is done in order to
		  subsequently average the errors and execution times, to assess performance of the algorithms.
		  
	Then the program ends, showing the total time needed for all the execution.
	
	
	
	4. Output
	
	What happens in the InOut directory is that in the Drop folder the folder related to the data set
	chosen in created. E.g. if the wine data set in chosen, the new folder WineDS is created in C\...\Drop\
	In \Drop\WineDS\ is created a Key.KEY file (readable with a text editor, such as Notepad++ or Sublime)
	reporting the translation key of the Categories string >> integers conversion (as explained in the report
	the ordinal is not mantained).
	Then another folder is created: DataPartitions, where the subfolders 
		
		- 10percentTestSets or
		- 33percentTestSets 
	
	are created, according to how keyboarded. In each of these the .dat reports are stored.
	
	These are 
	
		- Comparison between real categorical labels and guessed ones (computed by the learned
		  target function f : X --> Y) for each one of the used algorithms
		  
		- Errors and execution times. This is produced by APPENDING the new error/time couple for 
		  each run performed. So if such files are not deleted at the end of a 30 program runs, 
		  the next execution will append the new couple to the old one.
	
	-------------------------
	
	
	If things are done as described there should be no problem
	
