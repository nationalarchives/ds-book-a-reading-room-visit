<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ms="urn:schemas-microsoft-com:xslt" xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="Root">
    <body style="font-family: Arial, sans-serif;font-size: 16px;line-height: 24px;">
      <p>
        Dear <xsl:value-of select="Name" />,
      </p>
      <p>
        We are looking forward to seeing you soon. See your current document order below - if you would like to edit it, you can still do so before <b>
          <xsl:value-of select="CompleteByDate" /> at midnight, local UK time.
        </b>After this deadline has passed, we will begin preparing your document order.
      </p>

      <p>
        View or edit my document order: <xsl:value-of select="ReturnURL" />.
        You will need your booking reference and reader's ticket number to complete this step.
      </p>

      <h3 style="margin-top: 2em;">Your booking summary</h3>
		<p>
			Your booking reference is: <xsl:value-of select="BookingReference" /><br/>
			Your Reader’s ticket number is: <xsl:value-of select="ReaderTicket" />
		</p>
      <table style="text-align: left;border: 1px solid #999;border-collapse: collapse;margin: 2em 0;">
        <tbody>
          <tr>
            <th style="padding: 1em;border: 1px solid #999;width: 150px;vertical-align: top;">Visit type</th>
            <td style="padding: 1em;border: 1px solid #999;vertical-align: top;">
              <xsl:value-of select="VisitType" />
            </td>
          </tr>
          <tr>
            <th style="padding: 1em;border: 1px solid #999;width: 150px;vertical-align: top;">Visit date</th>
            <td style="padding: 1em;border: 1px solid #999;vertical-align: top;">
              <xsl:value-of select="VisitStartDate" />
            </td>
          </tr>
          <xsl:if test="string(ReadingRoom)">
            <tr>
              <th style="padding: 1em;border: 1px solid #999;width: 150px;vertical-align: top;">Reading room</th>
              <td style="padding: 1em;border: 1px solid #999;vertical-align: top;">
                <xsl:value-of select="ReadingRoom" />
              </td>
            </tr>
          </xsl:if>
        </tbody>
      </table>

      <h3 style="margin-top: 2em;">Your document order</h3>

      <xsl:for-each select="DocumentOrder">
        <p>
          <b>
            <xsl:value-of select="Label" />
          </b>
          <xsl:value-of select="Reference" /> : <xsl:value-of select="Description" />
        </p>
      </xsl:for-each>

      <xsl:for-each select="ReserveDocumentOrder">
        <p>
          <b>
            <xsl:value-of select="Label" />
          </b>
          <xsl:value-of select="Reference" /> : <xsl:value-of select="Description" />
        </p>
      </xsl:for-each>

      <h3 style="margin-top: 2em;">Cancel your visit</h3>
	  <p>
          You can cancel your visit at any time. Use this link: <xsl:value-of select="ReturnURL" />
	  </p>

	  <p>
	      This email inbox is not being monitored.
	  </p>
    </body>
  </xsl:template>
</xsl:stylesheet>
