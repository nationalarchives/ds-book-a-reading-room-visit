<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ms="urn:schemas-microsoft-com:xslt" xmlns:dt="urn:schemas-microsoft-com:datatypes">
	<xsl:output method="html" indent="yes"/>
	<xsl:template match="Root">
		<body style="font-family: Arial, sans-serif;font-size: 16px;line-height: 24px;">
			<p>
				Dear <xsl:value-of select="Name" />,
			</p>
			<p>
				Thank you for booking a visit to The National Archives.
			</p>
			<p>
        If you attended, we would love to know how your visit went by completing a visitor survey.
      </p>
			<p>
        During November 2024, this is a special survey in conjunction with the Archives and Records Association to help understand the visitor experience in archives across the country. If you already completed the paper version of this survey during your visit, you may disregard this email.
      </p>
			<p>
        However, even if you have completed a previous survey for The National Archives, or a similar survey recently at another archive, we would really appreciate your feedback on this visit. To take part, please <a href="https://www.smartsurvey.co.uk/s/7NTMNU/">click here</a>.
      </p>
			<p>
				This email inbox is not being monitored.
			</p>
		</body>
	</xsl:template>
</xsl:stylesheet>