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
        If you attended, we would love to know how your visit went by completing our short survey.
      </p>
      <p>
		  Please note, this survey is being run in conjunction with the Archives and Records Association between October and December 2022 to better understand the visitor journey, so even if you have completed a survey for The National Archives recently, or a similar survey at another archive, we would really appreciate your feedback on this visit. To take part, please
		  <a href="https://www.smartsurvey.co.uk/s/L4VHCP/">click here</a>.
      </p>
      <p>
        This email inbox is not being monitored.
      </p>
    </body>
  </xsl:template>
</xsl:stylesheet>