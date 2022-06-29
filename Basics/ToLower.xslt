<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output omit-xml-declaration="yes" indent="yes"/>
	
	<xsl:strip-space elements="*"/>

	<xsl:variable name="vUpper" select="'ABCDEFGHIJKLMNOPQRSTUVWXYZÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞŸŽŠŒ'"/>

	<xsl:variable name="vLower" select="'abcdefghijklmnopqrstuvwxyzàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþÿžšœ'"/>
		
	<xsl:template match="node()|@*">
		<xsl:copy>
			<xsl:apply-templates select="node()|@*"/>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="email/@classifier">
		<xsl:attribute name="classifier" namespace="{namespace-uri()}">
			
			<!--Führe eine ToLower Operation durch-->
			<xsl:variable name="LowerClassifier" select="translate(., $vUpper, $vLower)"/>
			
			<!--Erlaube nur die Werte 'priate', 'work' und 'other'-->
			<xsl:choose>
				<xsl:when test="($LowerClassifier='private') or ($LowerClassifier='work') or ($LowerClassifier='other')">
					<xsl:value-of select="$LowerClassifier"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="''"/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:attribute>
	</xsl:template>
	
</xsl:stylesheet>