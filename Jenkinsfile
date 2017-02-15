node('win7') {
	stage('Checkout') {
		checkout scm
	}
	stage('Build') {
		//bat 'nuget restore SolutionName.sln'
		bat "\"${tool 'MSBuild 14.0'}\" /p:Configuration=Release"		
    		//bat "\"${tool 'MSBuild'}\" SolutionName.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
	}
	stage('Archive') {
		archive '**/bin/Release/**'
	}
}
